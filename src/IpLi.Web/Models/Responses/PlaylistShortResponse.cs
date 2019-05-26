using System;
using IpLi.Core.Entities;

namespace IpLi.Web.Models.Responses
{
    public class PlaylistShortResponse
    {
        public PlaylistShortResponse(Playlist playlist)
        {
            Alias = playlist.Alias;
            Name = playlist.Name;
        }

        public String Alias { get; set; }
        public String Name { get; set; }
    }
}