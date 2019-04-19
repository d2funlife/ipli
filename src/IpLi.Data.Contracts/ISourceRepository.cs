using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.Data.Contracts
{
    public interface ISourceRepository
    {
        Task<Page<Source>> GetAsync(SourceQuery query,
                                    CancellationToken cancel = default);

        Task<Int32> AddRangeAsync(List<Source> sources,
                                  CancellationToken cancel = default);

        Task<Page<SourceAggregation>> GetAggregationByTitleAsync(SourceQuery query,
                                                            CancellationToken cancel = default);

        Task UpdateRange(List<Source> sources,
                         CancellationToken cancel = default);

        Task<Page<String>> GetAggregationTitlesAsync(SourceQuery query,
                                                     CancellationToken cancel = default);
    }
}