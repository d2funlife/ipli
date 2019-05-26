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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly String _filePath;

        public PlaylistRepository(String filePath)
        {
            _filePath = filePath;
        }

        public async Task<Playlist> CreateAsync(Playlist playlist,
                                                CancellationToken cancel = default)
        {
            var allPlaylists = await GetAllPlaylistsFromFileAsync(cancel);
            allPlaylists.Add(playlist.Alias, playlist);

            await SaveAllPlaylistsToFileAsync(allPlaylists, cancel);

            return playlist;
        }

        public async Task<Playlist> GetAsync(String alias,
                                             CancellationToken cancel)
        {
            var allPlaylists = await GetAllPlaylistsFromFileAsync(cancel);
            return allPlaylists.ContainsKey(alias)
                ? allPlaylists[alias]
                : null;
        }

        public async Task<Page<Playlist>> GetAsync(PlaylistQuery query,
                                                   CancellationToken cancel)
        {
            var allPlaylists = await GetAllPlaylistsFromFileAsync(cancel);
            return new Page<Playlist>
            {
                TotalCount = allPlaylists.Count,
                Items = allPlaylists.Select(x => x.Value).Skip(query.Offset).Take(query.Limit).ToList()
            };
        }

        private async Task<Dictionary<String, Playlist>> GetAllPlaylistsFromFileAsync(CancellationToken cancel)
        {
            if(!File.Exists(_filePath))
            {
                return new Dictionary<String, Playlist>(0);
            }

            var playlistDataString = await File.ReadAllTextAsync(_filePath, cancel);
            return JsonConvert.DeserializeObject<Dictionary<String, Playlist>>(playlistDataString);
        }

        private async Task SaveAllPlaylistsToFileAsync(Dictionary<String, Playlist> playlists,
                                                       CancellationToken cancel)
        {
            var data = JsonConvert.SerializeObject(playlists);
            await File.WriteAllTextAsync(_filePath, data, cancel);
        }
    }
}