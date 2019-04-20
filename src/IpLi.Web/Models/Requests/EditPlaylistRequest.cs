using System;
using System.Collections.Generic;
using IpLi.Core.Converters;
using IpLi.Core.Entities;

namespace IpLi.Web.Models.Requests
{
    public class EditPlaylistRequest
    {
        public String Name { get; set; }
        public List<String> Channels { get; set; }
        
        public Playlist ToDomain()
        {
            return new Playlist
            {
                Name = Name,
                Alias = StringToUrlStandard.ConvertWithLower(Name),
                Channels = Channels
            };
        }
    }
}