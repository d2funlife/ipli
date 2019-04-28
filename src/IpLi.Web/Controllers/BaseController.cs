using System;
using System.Threading;
using IpLi.Web.ActionResults;
using IpLi.Web.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IpLi.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        protected CancellationToken Cancel => HttpContext.RequestAborted;

        protected ActionResult InternalServerError()
        {
            return StatusCode(500);
        }

        protected void SetTotalCountHeader(Int32 total)
        {
            Response.Headers.Add("X-Total-Count", total.ToString());
        }

        public IActionResult M3uPlaylist(PlaylistResponse playlist)
        {
            return new M3uPlaylistResult(playlist);
        }
    }
}