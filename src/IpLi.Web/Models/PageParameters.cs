using System;

namespace IpLi.Web.Models
{
    public class PageParameters
    {
        public Int32 Offset { get; set; }
        
        public Int32 Limit { get; set; }
        
        public virtual String OrderBy { get; set; }
    }
}