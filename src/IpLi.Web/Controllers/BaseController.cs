using System;
using System.Threading;
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
        
        protected void SetTotalCountHeader(Int32 total) { Response.Headers.Add("X-Total-Count", total.ToString()); }
    }
}