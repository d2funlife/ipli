using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<ChannelResponse>>> Get([FromQuery]GetChannelsRequest request)
        {
            var page = await _channelManager.GetAsync(request.ToDomain(), Cancel);
            SetTotalCountHeader(page.TotalCount);
            return Ok(page.Items.Select(x => new ChannelResponse(x)));
        }

        [HttpGet("{alias}")]
        public async Task<ActionResult<ChannelResponse>> Get([FromRoute] String alias)
        {
            var channel = await _channelManager.GetAsync(alias, Cancel);
            if(channel == null)
            {
                return NotFound();
            }
            
            return Ok(new ChannelResponse(channel));
        }

        [HttpPost]
        public async Task<ActionResult<ChannelResponse>> Edit([FromBody]EditChannelRequest request)
        {
            var channel = await _channelManager.CreateOrUpdateAsync(request.ToDomain(), Cancel);
            return Ok(new ChannelResponse(channel));
        }

        [HttpDelete("{title}")]
        public async Task<ActionResult> Delete([FromRoute]String title)
        {
            return Ok();
        }
    }
}