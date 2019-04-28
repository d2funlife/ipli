using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;
using IpLi.Core.Queries;
using IpLi.Data.Contracts;
using Newtonsoft.Json;

namespace IpLi.Data.Json
{
    public class SourceRepository : ISourceRepository
    {
        private readonly String _filePath;

        public SourceRepository(String filePath)
        {
            _filePath = filePath;
        }

        public async Task<Page<Source>> GetAsync(SourceQuery query,
                                                 CancellationToken cancel = default)
        {
            var allSources = await GetAllSourcesFromFileAsync(cancel);

            if(!String.IsNullOrEmpty(query.Search))
            {
                allSources = allSources.Values.Where(x => x.Title.Contains(query.Search, StringComparison.OrdinalIgnoreCase))
                                       .ToDictionary(x => x.Id);
            }

            return new Page<Source>
            {
                TotalCount = allSources.Count,
                Items = allSources.Skip(query.Offset)
                                  .Take(query.Limit)
                                  .Select(x => x.Value)
                                  .ToList()
            };
        }

        public async Task<Page<SourceAggregation>> GetAggregationByTitleAsync(SourceQuery query,
                                                                              CancellationToken cancel = default)
        {
            var allSources = await GetAllSourcesFromFileAsync(cancel);

            var aggregateByTitle = allSources.Values
                                             .GroupBy(x => x.Title.ToLowerInvariant())
                                             .ToDictionary(x => x.Key, v => v.ToList());

            return new Page<SourceAggregation>
            {
                TotalCount = aggregateByTitle.Count,
                Items = aggregateByTitle.Skip(query.Offset)
                                        .Take(query.Limit)
                                        .Select(x => new SourceAggregation(x.Key, x.Value))
                                        .ToList()
            };
        }

        public async Task UpdateRange(List<Source> sources,
                                      CancellationToken cancel = default)
        {
            var allSources = await GetAllSourcesFromFileAsync(cancel);
            foreach (var source in sources)
            {
                if(!allSources.ContainsKey(source.Id))
                {
                    continue;
                }

                allSources[source.Id]
                   .Update(source);
            }

            await SaveAllSourcesToFileAsync(allSources, cancel);
        }

        public async Task<Page<String>> GetAggregationTitlesAsync(SourceQuery query,
                                                                  CancellationToken cancel = default)
        {
            var allSources = await GetAllSourcesFromFileAsync(cancel);

            var aggregateByTitle = allSources.Values.GroupBy(x => x.Title.ToLowerInvariant())
                                             .Select(x => x.Key)
                                             .ToList();
            return new Page<String>
            {
                TotalCount = aggregateByTitle.Count,
                Items = aggregateByTitle.Skip(query.Offset)
                                        .Take(query.Limit)
                                        .ToList()
            };
        }


        public async Task<Int32> AddRangeAsync(List<Source> sources,
                                               CancellationToken cancel = default)
        {
            var allSources = await GetAllSourcesFromFileAsync(cancel);
            var existUrls = allSources.Select(x => x.Value.Url)
                                      .ToHashSet();

            var addedSources = 0;
            foreach (var source in sources)
            {
                if(existUrls.Contains(source.Url))
                {
                    continue;
                }

                source.InitializeOnCreate();
                allSources.Add(source.Id, source);
                addedSources++;
            }

            await SaveAllSourcesToFileAsync(allSources, cancel);
            return addedSources;
        }


        private async Task<Dictionary<Guid, Source>> GetAllSourcesFromFileAsync(CancellationToken cancel)
        {
            if(!File.Exists(_filePath))
            {
                return new Dictionary<Guid, Source>(0);
            }

            var sourceDataString = await File.ReadAllTextAsync(_filePath, cancel);
            var sources = JsonConvert.DeserializeObject<Dictionary<Guid, Source>>(sourceDataString);
            return sources;
        }

        private async Task SaveAllSourcesToFileAsync(Dictionary<Guid, Source> sources,
                                                     CancellationToken cancel)
        {
            var sourcesString = JsonConvert.SerializeObject(sources);
            await File.WriteAllTextAsync(_filePath, sourcesString, cancel);
        }
    }
}