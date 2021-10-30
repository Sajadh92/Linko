using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Linko.Helper
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue, IRegisterSingleton 
    {
        private readonly ConcurrentQueue<Func<Task>> _workItems = new();

        private readonly SemaphoreSlim _signal = new(0);

        public void QueueBackgroundWorkItem(Func<Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);

            _signal.Release();
        }

        public async Task<Func<Task>> DequeueAsync()
        {
            await _signal.WaitAsync();

            _workItems.TryDequeue(out Func<Task> workItem);

            return workItem;
        }
    }
}
