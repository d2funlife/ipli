using System;
using System.Collections.Generic;
using System.Linq;
using IpLi.Core.Entities;

namespace IpLi.Web.Models.Responses
{
    public class PlaylistResponse
    {
        public String Alias { get; set; }
        public String Name { get; set; }
        public List<ChannelResponse> Channels { get; set; }

        public PlaylistResponse(Playlist playlist,
                                List<Channel> playlistChannels)
        {
            Alias = playlist.Alias;
            Name = playlist.Name;
            Channels = playlistChannels.Select(x => new ChannelResponse(x)).ToList();
        }

        public List<Channel> ToDomainChannels()
        {
            return Channels.Select(x => new Channel
                            {
                                Alias = x.Alias,
                                Title = x.Title,
                                CurrentSourceUrl = x.CurrentSourceUrl,
                                AlternativeTitles = x.AlternativeTitles,
                                ImageUrl = x.ImageUrl
                            })
                           .ToList();
        }
    }
}