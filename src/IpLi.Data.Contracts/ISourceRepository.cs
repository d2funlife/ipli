using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;

namespace IpLi.Data.Contracts
{
    public interface ISourceRepository
    {
        Task<Int32> AddRangeAsync(List<Source> sources,
                                  CancellationToken cancel = default);
    }
}