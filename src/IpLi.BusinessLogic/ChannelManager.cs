using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.BusinessLogic
{
    public class ChannelManager : IChannelManager
    {
        public Task<Page<Channel>> GetAsync(ChannelQuery query,
                             CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Channel> GetAsync(String title,
                             CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Channel> CreateAsync(Channel channel,
                                CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Channel> UpdateAsync(Channel channel,
                                CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(String title,
                                CancellationToken cancel = default)
        {
            throw new NotImplementedException();
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