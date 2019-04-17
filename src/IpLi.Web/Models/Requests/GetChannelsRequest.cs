using IpLi.Core.Queries;

namespace IpLi.Web.Models.Requests
{
    public class GetChannelsRequest : PageParameters
    {
        public ChannelQuery ToDomain()
        {
            return new ChannelQuery
            {
                Limit = Limit,
                Offset = Offset
            };
        }
    }
}