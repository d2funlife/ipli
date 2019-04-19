using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Core.Queries;
using IpLi.Data.Contracts;
using IpLi.Serializers;

namespace IpLi.BusinessLogic
{
    public class SourceManager : ISourceManager
    {
        private ISourceRepository _sourceRepository;

        public SourceManager(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public Task<Page<Source>> GetAsync(SourceQuery query,
                                           CancellationToken cancel = default)
        {
            return _sourceRepository.GetAsync(query, cancel);
        }

        public Task<Source> GetAsync(String title,
                                     CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Source> CreateAsync(Source playlist,
                                        CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Source> UpdateAsync(Source playlist,
                                        CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(String title,
                                CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Int32> ImportSourcesAsync(Stream playlistStream,
                                                    CancellationToken cancel = default)
        {
            using (var reader = new StreamReader(playlistStream))
            {
                var playlistText = await reader.ReadToEndAsync();
                var sources = M3uSerializer.Deserialize(playlistText);
                return await _sourceRepository.AddRangeAsync(sources, cancel);
            }
        }

        public Task<Page<SourceAggregation>> GetAggregationByTitleAsync(SourceQuery query,
                                          CancellationToken cancel = default)
        {
            return _sourceRepository.GetAggregationByTitleAsync(query, cancel);
        }

        public Task<Page<String>> GetAggregationTitlesAsync(SourceQuery query,
                                              CancellationToken cancel = default)
        {
            return _sourceRepository.GetAggregationTitlesAsync(query, cancel);
        }
    }
}