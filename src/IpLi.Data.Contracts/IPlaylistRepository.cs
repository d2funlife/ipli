using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.Data.Contracts
{
    public interface IPlaylistRepository
    {
        Task<Playlist> CreateAsync(Playlist playlist,
                                   CancellationToken cancel = default);

        Task<Playlist> GetAsync(String alias,
                                CancellationToken cancel);

        Task<Page<Playlist>> GetAsync(PlaylistQuery query,
                                      CancellationToken cancel);
    }
}