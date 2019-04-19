using System;
using System.Collections.Generic;
using IpLi.Core.Entities;

namespace IpLi.Web.Models.Responses
{
    public class SourceAggregationResponse
    {
        public SourceAggregationResponse(String title,
                                         List<Source> sources)
        {
            Title = title;
            Sources = sources;
        }

        public String Title { get; set; }
        public List<Source> Sources { get; set; }
    }
}