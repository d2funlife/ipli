using System;
using IpLi.Core.Queries;

namespace IpLi.Web.Models.Requests
{
    public class GetSourcesRequest : PageParameters
    {
        public String Search { get; set; }
        
        public SourceQuery ToDomain()
        {
            return new SourceQuery(Offset, Limit)
            {
                Search = Search
            };
        }
    }
}