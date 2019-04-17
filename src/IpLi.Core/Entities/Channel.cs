using System;

namespace IpLi.Core.Entities
{
    public class Channel
    {
        public String Title { get; set; }
        public String[] AlternativeTitles { get; set; }
        public String ImgUrl { get; set; }

        public Source[] Sources { get; set; }
    }
}