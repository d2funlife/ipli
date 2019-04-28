using System;
using System.Threading;
using System.Threading.Tasks;
using IpLi.Core.Contracts;
using Microsoft.Extensions.Hosting;

namespace IpLi.Web.Services
{
    public class BackgroundSourceScanner : IHostedService
    {
        private readonly ISourceScanner _sourceScanner;

        private volatile Boolean _isTaskStopRequested;
        private System.Timers.Timer _taskTimer;
        private CancellationToken _cancel;
        
        public BackgroundSourceScanner(ISourceScanner sourceScanner)
        {
            _sourceScanner = sourceScanner;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancel = cancellationToken;
            _taskTimer = new System.Timers.Timer(TimeSpan.FromDays(3).TotalMilliseconds)
            {
                AutoReset = false
            };
            _taskTimer.Elapsed += ProcessTask;

            Task.Run(() => ProcessTask(null, null), cancellationToken);
            
            return Task.CompletedTask;
        }
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _isTaskStopRequested = true;
            _taskTimer.Stop();
            return Task.CompletedTask;
        }
        
        private void ProcessTask(Object sender,
                                 System.Timers.ElapsedEventArgs e)
        {
            _sourceScanner.ScanAllAsync(_cancel).GetAwaiter().GetResult();
            _sourceScanner.SetSourcesToAllChannelsAsync(_cancel).GetAwaiter().GetResult();
            
            if(!_isTaskStopRequested)
            {
                _taskTimer.Start();
            }
        }
    }
}