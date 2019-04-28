using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpLi.Core.Entities;
using m3uParser;

namespace IpLi.Serializers
{
    public static  class M3uSerializer
    {
        public static String Serialize(IEnumerable<Channel> channels)
        {
            var content = new StringBuilder(50);
            content.AppendLine("#EXTM3U");
            foreach (var channel in channels)
            {
                content.Append("#EXTINF:-1,")
                       .AppendLine(channel.Title);
                content.AppendLine(channel.CurrentSourceUrl);
            }

            return content.ToString();
        }

        public static List<Source> Deserialize(String playlistText)
        {
            var m3uPLaylist = M3U.Parse(playlistText);

            var result = new List<Source>(m3uPLaylist.Medias.Count());
            foreach (var media in m3uPLaylist.Medias)
            {
                result.Add(new Source
                {
                    Title = media.Title.InnerTitle,
                    Url = media.MediaFile,
                });
            }
            
            return result;
        }
    }
}