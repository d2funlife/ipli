using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.Core.Contracts
{
    public interface IPlaylistManager
    {
        Task<Page<Playlist>> GetAsync(PlaylistQuery query,
                                      CancellationToken cancel = default);

        Task<Playlist> GetAsync(String name,
                                CancellationToken cancel = default);

        Task<Playlist> CreatePlaylistAsync(Playlist playlist,
                                           CancellationToken cancel = default);
        
        Task<Playlist> UpdatePlaylistAsync(Playlist playlist,
                                           CancellationToken cancel = default);
        
        Task DeleteAsync(String name,
                         CancellationToken cancel = default);
    }
}