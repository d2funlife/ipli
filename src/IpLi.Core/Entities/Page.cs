using System;
using System.Collections.Generic;

namespace IpLi.Core.Entities
{
    public class Page<T>
    {
        public Page()
        {
        }

        public Page(List<T> items,
                    Int32 totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public List<T> Items { get; set; }
        public Int32 TotalCount { get; set; }
    }
}