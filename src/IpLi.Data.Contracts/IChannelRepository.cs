using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.Data.Contracts
{
    public interface IChannelRepository
    {
        Task<Channel> GetAsync(String alias,
                               CancellationToken cancel = default);

        Task<Page<Channel>> GetAsync(ChannelQuery query,
                                     CancellationToken cancel = default);

        Task<Channel> AddAsync(Channel channel,
                               CancellationToken cancel = default);

        Task<Channel> UpdateAsync(Channel channel,
                                  CancellationToken cancel = default);

        Task DeleteAsync(String alias,
                         CancellationToken cancel = default);
    }
}