using IpLi.Core.Queries;

namespace IpLi.Web.Models.Requests
{
    public class GetPlaylistsRequest : PageParameters
    {
        public PlaylistQuery ToDomain()
        {
            return new PlaylistQuery(Offset, Limit);
        }
    }
}