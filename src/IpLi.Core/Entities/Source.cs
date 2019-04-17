using System;

namespace IpLi.Core.Entities
{
    public class Source
    {
        public String Title { get; set; }
        public String Url { get; set; }
        public Int32? PriorityNumber { get; set; }
        public DateTime? LastScanDate { get; set; }
        public Int32? FrameSize { get; set; }
    }
}