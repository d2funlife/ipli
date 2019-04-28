using System;
using IpLi.Core.Converters;

namespace IpLi.Core.Queries
{
    public class ChannelQuery : BaseQuery
    {
        public ChannelQuery(Int32 offset = DefaultOffset,
                            Int32 limit = DefaultLimit)
            : base(offset, limit)
        {
        }

        public static ChannelQuery GetAll()
        {
            var query = new ChannelQuery();
            query.SetLimit(Int32.MaxValue);
            return query;
        }
    }
}