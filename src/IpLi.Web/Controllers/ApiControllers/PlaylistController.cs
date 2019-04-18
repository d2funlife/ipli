using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IpLi.Web.Controllers.ApiControllers
{
    [Route("api/playlists")]
    public class PlaylistController : BaseController
    {
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
    }
}