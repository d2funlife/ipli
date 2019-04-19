using System;
using IpLi.Core.Entities;

namespace IpLi.Web.Models.Responses
{
    public class SourceResponse
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Url { get; set; }
        public Int32? PriorityNumber { get; set; }
        public DateTime? LastScanDate { get; set; }
        public Int32? FrameSize { get; set; }

        public SourceResponse(Source source)
        {
            Id = source.Id;
            Title = source.Title;
            Url = source.Url;
            PriorityNumber = source.PriorityNumber;
            LastScanDate = source.LastScanDate;
            FrameSize = source.FrameSize;
        }
    }
}