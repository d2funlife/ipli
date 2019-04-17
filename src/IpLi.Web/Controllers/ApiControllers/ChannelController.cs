using System;
using System.Linq;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Web.Models.Requests;
using IpLi.Web.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IpLi.Web.Controllers.ApiControllers
{
    [Route("api/channels")]
    public class ChannelController : BaseController
    {
        private readonly IChannelManager _channelManager;

        public ChannelController(IChannelManager channelManager)
        {
            _channelManager = channelManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetChannels([FromQuery]GetChannelsRequest request)
        {
            var page = await _channelManager.GetAsync(request.ToDomain(), Cancel);
            SetTotalCountHeader(page.TotalCount);
            return Ok(page.Items.Select(x => new ChannelResponse(x)));
        }

        [HttpPost]
        public async Task<ActionResult> EditChannel([FromBody]EditChannelRequest request)
        {
            var channel = await _channelManager.CreateOrUpdateAsync(request.ToDomain(), Cancel);
            return Ok(new ChannelResponse(channel));
        }
    }
}