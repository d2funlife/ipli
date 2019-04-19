using System;

namespace IpLi.Core.Queries
{
    public class SourceQuery : BaseQuery
    {
        public SourceQuery(Int32 offset = DefaultOffset,
                           Int32 limit = DefaultLimit) : base(offset, limit)
        {
        }
    }
}