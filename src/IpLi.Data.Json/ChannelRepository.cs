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
    public class ChannelRepository : IChannelRepository
    {
        private readonly String _filePath;

        public ChannelRepository(String filePath)
        {
            _filePath = filePath;
        }

        private async Task<Dictionary<String, Channel>> GetAllChannelsFromFileAsync(CancellationToken cancel)
        {
            if(!File.Exists(_filePath))
            {
                return new Dictionary<String, Channel>(0);
            }

            var channelsDataString = await File.ReadAllTextAsync(_filePath, cancel);
            var channels = JsonConvert.DeserializeObject<Dictionary<String, Channel>>(channelsDataString);
            return channels;
        }

        private async Task SaveAllChannelsToFileAsync(Dictionary<String, Channel> channels,
                                                      CancellationToken cancel)
        {
            var channelsString = JsonConvert.SerializeObject(channels);
            await File.WriteAllTextAsync(_filePath, channelsString, cancel);
        }

        public async Task<Channel> GetAsync(String alias,
                                            CancellationToken cancel = default)
        {
            var allChannels = await GetAllChannelsFromFileAsync(cancel);
            if(!allChannels.ContainsKey(alias))
            {
                return null;
            }

            return allChannels[alias];
        }

        public async Task<List<Channel>> GetAsync(List<String> titles,
                                                  CancellationToken cancel = default)
        {
            var allChannels = await GetAllChannelsFromFileAsync(cancel);
            var result = new List<Channel>(titles.Count);
            foreach (var title in titles)
            {
                var channel = allChannels.Values.FirstOrDefault(x => x.Title
                                                                      .ToLowerInvariant()
                                                                      .Equals(title.ToLowerInvariant()));
                if(channel != null)
                {
                    result.Add(channel);
                }
            }

            return result;
        }

        public async Task<Page<Channel>> GetAsync(ChannelQuery query,
                                                  CancellationToken cancel = default)
        {
            var allChannels = await GetAllChannelsFromFileAsync(cancel);

            var page = new Page<Channel>
            {
                Items = allChannels.Skip(query.Offset)
                                   .Take(query.Limit)
                                   .Select(x => x.Value)
                                   .ToList(),
                TotalCount = allChannels.Count
            };

            return page;
        }

        public async Task<Channel> AddAsync(Channel channel,
                                            CancellationToken cancel = default)
        {
            var allChannels = await GetAllChannelsFromFileAsync(cancel);
            allChannels.Add(channel.Alias, channel);

            await SaveAllChannelsToFileAsync(allChannels, cancel);
            return channel;
        }

        public async Task<Channel> UpdateAsync(Channel channel,
                                               CancellationToken cancel = default)
        {
            var allChannels = await GetAllChannelsFromFileAsync(cancel);
            allChannels[channel.Alias] = channel;

            await SaveAllChannelsToFileAsync(allChannels, cancel);
            return channel;
        }

        public async Task DeleteAsync(String alias,
                                      CancellationToken cancel = default)
        {
            var allChannels = await GetAllChannelsFromFileAsync(cancel);
            allChannels.Remove(alias);

            await SaveAllChannelsToFileAsync(allChannels, cancel);
        }
    }
}