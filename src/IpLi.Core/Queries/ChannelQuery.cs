using System;

namespace IpLi.Core.Queries
{
    public class ChannelQuery : BaseQuery
    {
        public ChannelQuery(Int32 offset = DefaultOffset,
                            Int32 limit = DefaultLimit)
            : base(offset, limit)
        {
        }
    }
}