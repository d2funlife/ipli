using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Core.Queries;
using IpLi.Data.Contracts;

namespace IpLi.BusinessLogic
{
    public class PlaylistManager : IPlaylistManager
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistManager(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

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

        public Task<Playlist> CreateAsync(Playlist playlist,
                                          CancellationToken cancel = default)
        {
            return _playlistRepository.CreateAsync(playlist, cancel);
        }

        public Task<Playlist> UpdateAsync(Playlist playlist,
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