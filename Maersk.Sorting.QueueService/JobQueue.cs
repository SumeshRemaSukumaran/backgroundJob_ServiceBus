using Maersk.Sorting.Model.Enum;
using Maersk.Sorting.Model.ViewModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maersk.Sorting.QueueService
{
    public class JobQueue
    {
        private readonly object queueLock = new object();
        public LinkedList<SortJobModel> Items { get; }
        private readonly ILogger<JobQueue> _logger;

        public JobQueue(
       IBackgroundTaskQueue taskQueue,
       ILogger<JobQueue> logger,
       IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            Items = new LinkedList<SortJobModel>();
        }
        public void EnqueueJob(SortJobModel item)
        {
            var value = new LinkedListNode<SortJobModel>(item);
            if (Items.First == null)
            {
                lock (queueLock)
                {
                    Items.AddFirst(value);
                }
            }
            else
            {
                lock (queueLock)
                {
                    Items.AddLast(value);
                }
            }
        }
        public void Dequeue()
        {
            if (Items.Any())
            {
                lock (queueLock)
                {
                    Items.RemoveFirst();
                }
            }
        }
    }
}
