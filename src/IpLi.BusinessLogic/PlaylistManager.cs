using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.BusinessLogic
{
    public class PlaylistManager : IPlaylistManager
    {
        public Task<Page<Playlist>> GetAsync(PlaylistQuery query,
                             CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> GetAsync(String name,
                             CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> CreatePlaylistAsync(Playlist playlist,
                                        CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> UpdatePlaylistAsync(Playlist playlist,
                                        CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(String name,
                                CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}