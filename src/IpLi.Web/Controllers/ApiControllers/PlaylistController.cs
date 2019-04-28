using System;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Web.Models.Requests;
using IpLi.Web.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IpLi.Web.Controllers.ApiControllers
{
    [Route("api/playlists")]
    public class PlaylistController : BaseController
    {
        private readonly IPlaylistManager _playlistManager;
        private readonly IChannelManager _channelManager;

        public PlaylistController(IPlaylistManager playlistManager,
                                  IChannelManager channelManager)
        {
            _playlistManager = playlistManager;
            _channelManager = channelManager;
        }

        [HttpGet("{alias}")]
        public async Task<ActionResult<PlaylistResponse>> Get([FromRoute] String alias)
        {
            var playlist = await _playlistManager.GetAsync(alias, Cancel);
            var playlistChannels = await _channelManager.GetAsync(playlist.Channels, Cancel);
            return Ok(new PlaylistResponse(playlist, playlistChannels));
        }

        [HttpGet("{alias}.m3u")]
        public async Task<IActionResult> GetFile([FromRoute] String alias)
        {
            var playlist = await _playlistManager.GetAsync(alias, Cancel);
            var playlistChannels = await _channelManager.GetAsync(playlist.Channels, Cancel);
            return M3uPlaylist(new PlaylistResponse(playlist, playlistChannels));
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistResponse>> Edit([FromBody] EditPlaylistRequest request)
        {
            var playlist = await _playlistManager.CreateAsync(request.ToDomain(), Cancel);
            var playlistChannels = await _channelManager.GetAsync(playlist.Channels, Cancel);
            return Ok(new PlaylistResponse(playlist, playlistChannels));
        }
    }
}