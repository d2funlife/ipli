using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpLi.Serializers;
using IpLi.Web.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IpLi.Web.ActionResults
{
    public class M3uPlaylistResult : IActionResult
    {
        private readonly PlaylistResponse _playlist;

        public M3uPlaylistResult(PlaylistResponse playlist)
        {
            _playlist = playlist;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var channels = _playlist.ToDomainChannels().Where(x => x.CurrentSourceUrl != null);
            var playlistString = M3uSerializer.Serialize(channels);
            var playListBytes = Encoding.UTF8.GetBytes(playlistString);
            
            var response = context.HttpContext.Response;
            response.ContentType = "audio/x-mpegurl";
            response.StatusCode = StatusCodes.Status200OK;
            await context.HttpContext.Response.Body.WriteAsync(playListBytes);
        }
    }
}