using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Core.Exceptions;
using IpLi.Core.Queries;
using IpLi.Data.Contracts;

namespace IpLi.BusinessLogic
{
    public class ChannelManager : IChannelManager
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelManager(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public Task<Page<Channel>> GetAsync(ChannelQuery query,
                                            CancellationToken cancel = default)
        {
            return _channelRepository.GetAsync(query, cancel);
        }

        public Task<Channel> GetAsync(String alias,
                                      CancellationToken cancel = default)
        {
            return _channelRepository.GetAsync(alias, cancel);
        }

        public Task<Channel> CreateAsync(Channel channel,
                                         CancellationToken cancel = default)
        {
            return _channelRepository.AddAsync(channel, cancel);
        }

        public async Task<Channel> UpdateAsync(Channel channel,
                                         CancellationToken cancel = default)
        {
            var existChannel = await _channelRepository.GetAsync(channel.Title, cancel);
            if(existChannel == null)
            {
                throw new EntityNotFoundException();
            }

            existChannel.Update(channel);
            
            return await _channelRepository.UpdateAsync(channel, cancel);
        }

        public async Task<Channel> CreateOrUpdateAsync(Channel channel,
                                                       CancellationToken cancel = default)
        {
            var existChannel = await _channelRepository.GetAsync(channel.Title, cancel);
            if(existChannel == null)
            {
                return await CreateAsync(channel, cancel);
            }
            else
            {
                return await UpdateAsync(channel, cancel);
            }
        }

        public Task DeleteAsync(String alias,
                                CancellationToken cancel = default)
        {
            return _channelRepository.DeleteAsync(alias, cancel);
        }
        

        public Task<Channel> AddSource(Source source,
                                       CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Channel> DeleteSource(Source source,
                                          CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}