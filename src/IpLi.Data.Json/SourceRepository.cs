using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;
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

        public async Task<Int32> AddRangeAsync(List<Source> sources,
                                               CancellationToken cancel = default)
        {
            var existSources = await GetAllSourcesFromFileAsync(cancel);
            var existUrls = existSources.Select(x => x.Value.Url)
                                       .ToHashSet();

            var addedSources = 0;
            foreach (var source in sources)
            {
                if(existUrls.Contains(source.Url))
                {
                    continue;
                }
                
                source.InitializeOnCreate();
                existSources.Add(source.Id, source);
                addedSources++;
            }

            await SaveAllSourcesToFileAsync(existSources, cancel);
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