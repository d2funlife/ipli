using System;
using System.Web;
using IpLi.Core.Entities;

namespace IpLi.Web.Models.Responses
{
    public class ChannelResponse
    {
        public String Alias { get; set; }
        public String Title { get; set; }
        public String[] AlternativeTitles { get; set; }
        public String ImageUrl { get; set; }
        
        public ChannelResponse(Channel channel)
        {
            Alias = channel.Alias;
            Title = channel.Title;
            AlternativeTitles = channel.AlternativeTitles;
            ImageUrl = channel.ImageUrl;
        }
    }
}