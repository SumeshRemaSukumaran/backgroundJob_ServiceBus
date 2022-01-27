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
        public LinkedList<SortJobModel> SortJob { get; }
        private readonly ILogger<JobQueue> _logger;

        public JobQueue(ILogger<JobQueue> logger)
        {
            _logger = logger;
            SortJob = new LinkedList<SortJobModel>();
        }
        public void EnqueueJob(SortJobModel item)
        {
            var value = new LinkedListNode<SortJobModel>(item);
            if (SortJob.First == null)
            {
                lock (queueLock)
                {
                    SortJob.AddFirst(value);
                }
            }
            else
            {
                lock (queueLock)
                {
                    SortJob.AddLast(value);
                }
            }
            _logger.LogInformation("Job {jobId} added to Queue ", item.Id);
        }
        public void Dequeue()
        {
            if (SortJob.Any())
            {
                lock (queueLock)
                {
                    SortJob.RemoveFirst();
                }
            }
        }
    }
}
