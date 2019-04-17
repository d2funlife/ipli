using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using IpLi.Core.Entities;
using IpLi.Core.Queries;

namespace IpLi.BusinessLogic
{
    public class SourceManager : ISourceManager
    {
        public Task<Page<Source>> GetAsync(SourceQuery query,
                             CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Source> GetAsync(String title,
                             CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Source> CreateAsync(Source playlist,
                                CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Source> UpdateAsync(Source playlist,
                                CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(String title,
                                CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}