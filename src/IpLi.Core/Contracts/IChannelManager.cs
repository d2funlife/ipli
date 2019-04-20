using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.Core.Contracts
{
    public interface IChannelManager
    {
        Task<Page<Channel>> GetAsync(ChannelQuery query,
                                     CancellationToken cancel = default);

        Task<Channel> GetAsync(String alias,
                               CancellationToken cancel = default);

        Task<List<Channel>> GetAsync(List<String> aliases,
                                     CancellationToken cancel = default);

        Task<Channel> CreateAsync(Channel channel,
                                  CancellationToken cancel = default);

        Task<Channel> UpdateAsync(Channel channel,
                                  CancellationToken cancel = default);

        Task<Channel> CreateOrUpdateAsync(Channel channel,
                                          CancellationToken cancel = default);

        Task DeleteAsync(String alias,
                         CancellationToken cancel = default);

        Task<Channel> AddSource(Source source,
                                CancellationToken cancel = default);

        Task<Channel> DeleteSource(Source source,
                                   CancellationToken cancel = default);
    }
}