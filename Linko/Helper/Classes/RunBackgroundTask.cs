using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Linko.Helper
{
    public class RunBackgroundTask : BackgroundService
    {
        private readonly ILoggerRepository _logger;
        private readonly IBackgroundTaskQueue TaskQueue;

        public RunBackgroundTask(
            ILoggerRepository logger,
            IBackgroundTaskQueue taskQueue)
        {
            _logger = logger;
            TaskQueue = taskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Func<Task> workItem = await TaskQueue.DequeueAsync();

                try
                {
                    await workItem();
                }
                catch (Exception ex)
                {
                    await _logger.WriteAsync(ex, $"RunBackgroundTask => BackgroundProcessing => {nameof(workItem)}");
                }
            }
        }
    }
}
