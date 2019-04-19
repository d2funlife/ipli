using System.Threading;
using System.Threading.Tasks;

namespace IpLi.Core.Contracts
{
    public interface ISourceScanner
    {
        Task ScanAllAsync(CancellationToken cancel = default);
    }
}