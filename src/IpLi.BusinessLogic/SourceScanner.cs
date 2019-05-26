using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Core.Helpers;
using IpLi.Core.Queries;
using IpLi.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace IpLi.BusinessLogic
{
    public class SourceScanner : ISourceScanner
    {
        private readonly ILogger<SourceScanner> _logger;
        private readonly ISourceRepository _sourceRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IHttpClientFactory _clientFactory;

        public SourceScanner(ISourceRepository sourceRepository,
                             ILogger<SourceScanner> logger,
                             IHttpClientFactory clientFactory,
                             IChannelRepository channelRepository)
        {
            _sourceRepository = sourceRepository;
            _logger = logger;
            _clientFactory = clientFactory;
            _channelRepository = channelRepository;
        }

        public async Task ScanAllAsync(CancellationToken cancel = default)
        {
            var sources = await _sourceRepository.GetAsync(SourceQuery.GetAll(), cancel);

            var buffer = new Byte[32768];
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

        public async Task SetSourcesToAllChannelsAsync(CancellationToken cancel = default)
        {
//            var pageSize = 500;
//            var channels = await _channelRepository.GetAsync(new ChannelQuery(0, pageSize), cancel);
//            var totalPages = (Int32) (Math.Ceiling(channels.TotalCount / (Double) pageSize));
//            for (var i = 1; i < totalPages; i++)
//            {
//            }


            var allChannels = await _channelRepository.GetAsync(ChannelQuery.GetAll(), cancel);
            var allSourcesAggregated = await _sourceRepository.GetAggregationByTitleAsync(SourceQuery.GetAll(), cancel);
            var sources = allSourcesAggregated.Items.ToDictionary(x => x.Title, v => v.Sources);
            foreach (var channel in allChannels.Items)
            {
                if(channel.LockSourceUrl)
                {
                    continue;
                }

                var titleLower = channel.Title.ToLowerInvariant();

                List<Source> processingSources = new List<Source>(1);
                if(sources.ContainsKey(titleLower))
                {
                    processingSources = sources[titleLower];
                }

                if(channel.AlternativeTitles.IsNotEmpty())
                {
                    foreach (var alternativeTitle in channel.AlternativeTitles)
                    {
                        var alternativeTitleLower = alternativeTitle.ToLowerInvariant();
                        {
                            if(sources.ContainsKey(alternativeTitleLower))
                            {
                                processingSources.AddRange(sources[alternativeTitleLower]);
                            }
                        }
                    }
                }

                processingSources = processingSources.Where(x => x.FrameSize > 0).ToList();
                if(processingSources.IsEmpty())
                {
                    continue;
                }

                var actualSource = processingSources.OrderBy(x => x.PriorityNumber)
                                                    .ThenByDescending(x => x.LastScanDate)
                                                    .ThenByDescending(x => x.FrameSize)
                                                    .First();

                channel.CurrentSourceUrl = actualSource.Url;
                await _channelRepository.UpdateAsync(channel, cancel);
            }
        }
    }
}