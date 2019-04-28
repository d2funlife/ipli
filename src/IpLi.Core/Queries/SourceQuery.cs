using System;

namespace IpLi.Core.Queries
{
    public class SourceQuery : BaseQuery
    {
        public String Search { get; set; }
        
        public SourceQuery(Int32 offset = DefaultOffset,
                           Int32 limit = DefaultLimit) : base(offset, limit)
        {
        }

        public static SourceQuery GetAll()
        {
            var query = new SourceQuery();
            query.SetLimit(Int32.MaxValue);
            return query;
        }
    }
}