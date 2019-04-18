using System;
using IpLi.Core.Converters;
using IpLi.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IpLi.Web.Models.Requests
{
    public class EditChannelRequest
    {
        public String Title { get; set; }
        
        public String[] AlternativeTitles { get; set; }

        public String ImageUrl { get; set; }

        public Channel ToDomain()
        {
            return new Channel
            {
                Alias = StringToUrlStandard.ConvertWithLower(Title),
                Title = Title,
                AlternativeTitles = AlternativeTitles,
                ImageUrl = ImageUrl
            };
        }
    }
}