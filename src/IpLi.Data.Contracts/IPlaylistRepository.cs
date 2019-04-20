using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;

namespace IpLi.Data.Contracts
{
    public interface IPlaylistRepository
    {
        Task<Playlist> CreateAsync(Playlist playlist,
                                   CancellationToken cancel = default);
    }
}