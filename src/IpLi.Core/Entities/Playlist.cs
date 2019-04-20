using System;
using System.Collections.Generic;

namespace IpLi.Core.Entities
{
    public class Playlist
    {
        public String Alias { get; set; }
        public String Name { get; set; }
        public List<String> Channels { get; set; }
    }
}