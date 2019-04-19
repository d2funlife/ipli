using IpLi.Core.Queries;

namespace IpLi.Web.Models.Requests
{
    public class GetSourcesRequest : PageParameters
    {
        public SourceQuery ToDomain()
        {
            return new SourceQuery(Offset, Limit);
        }
    }
}