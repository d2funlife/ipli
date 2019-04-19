using System;
using System.Collections.Generic;

namespace IpLi.Core.Entities
{
    public class SourceAggregation
    {
        public String Title { get; set; }
        public List<Source> Sources { get; set; }
        
        public SourceAggregation(String title,
                                 List<Source> sources)
        {
            Title = title;
            Sources = sources;
        }
    }
}