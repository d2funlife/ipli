using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Core.Queries;
using IpLi.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace IpLi.BusinessLogic
{
    public class SourceScanner : ISourceScanner
    {
        private readonly ILogger<SourceScanner> _logger;
        private readonly ISourceRepository _sourceRepository;
        private readonly IHttpClientFactory _clientFactory;

        public SourceScanner(ISourceRepository sourceRepository,
                             ILogger<SourceScanner> logger,
                             IHttpClientFactory clientFactory)
        {
            _sourceRepository = sourceRepository;
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task ScanAllAsync(CancellationToken cancel = default)
        {
            var sources = await _sourceRepository.GetAsync(SourceQuery.GetMax(), cancel);

            var buffer = new Byte[8192];
            var http = _clientFactory.CreateClient();
            http.Timeout = TimeSpan.FromSeconds(5);

            var now = DateTime.Now;
            
            var pageSize = 1000;
            var totalPages = (Int32) Math.Ceiling(sources.TotalCount / (Double) pageSize);
            for (var i = 1; i < totalPages; i++)
            {
                Parallel.ForEach(sources.Items,
                                 source =>
                                 {
                                     if(source.LastScanDate.HasValue &&
                                        (source.LastScanDate.Value - DateTime.Now).TotalHours < 5)
                                     {
                                         return;
                                     }
                                     
                                     source.LastScanDate = now;

                                     try
                                     {
                                         _logger.LogInformation($"Try to get live for : {source.Title}");

                                         var stream = http.GetStreamAsync(source.Url)
                                                          .GetAwaiter()
                                                          .GetResult();
                                         
                                         var tokenSource = new CancellationTokenSource();
                                         tokenSource.CancelAfter(TimeSpan.FromSeconds(5));
                                         var bytesFromStream = stream.ReadAsync(buffer, tokenSource.Token)
                                                                     .GetAwaiter()
                                                                     .GetResult();
                                         source.FrameSize = bytesFromStream;
                                         _logger.LogInformation($"Frame for {source.Title} | {source.Url} : {source.FrameSize}");
                                     }
                                     catch (Exception e)
                                     {
                                         _logger.LogError(e, $"Error on source scanning {source.Title}");
                                     }
                                 });

                await _sourceRepository.UpdateRange(sources.Items, cancel);
                sources = await _sourceRepository.GetAsync(new SourceQuery(i * pageSize, pageSize), cancel);
            }
            
            _logger.LogInformation("Complete sources scaning");
        }
    }
}