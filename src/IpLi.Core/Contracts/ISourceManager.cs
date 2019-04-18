using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.Core.Contracts
{
    public interface ISourceManager
    {
        Task<Page<Source>> GetAsync(SourceQuery query,
                                    CancellationToken cancel = default);

        Task<Source> GetAsync(String title,
                              CancellationToken cancel = default);

        Task<Source> CreateAsync(Source playlist,
                                 CancellationToken cancel = default);

        Task<Source> UpdateAsync(Source playlist,
                                 CancellationToken cancel = default);

        Task DeleteAsync(String title,
                         CancellationToken cancel = default);

        Task<Int32> ImportSourcesAsync(Stream playlistStream,
                                       CancellationToken cancel = default);
    }
}