using System;
using System.Threading.Tasks;

namespace Linko.Helper
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<Task> workItem);

        Task<Func<Task>> DequeueAsync();
    }
}
