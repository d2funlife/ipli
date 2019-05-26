using System;

namespace IpLi.Core.Queries
{
    public class PlaylistQuery : BaseQuery
    {
        public PlaylistQuery(Int32 offset = DefaultOffset,
                             Int32 limit = DefaultLimit) : base(offset, limit)
        {
        }
    }
}