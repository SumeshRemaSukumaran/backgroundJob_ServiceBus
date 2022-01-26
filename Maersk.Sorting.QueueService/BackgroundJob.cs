using Maersk.Sorting.Model.Enum;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Maersk.Sorting.QueueService
{
    public class BackgroundJob
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<BackgroundJob> _logger;
        private readonly CancellationToken _cancellationToken;
        private readonly JobQueue _jobQueue;

        public BackgroundJob(
            IBackgroundTaskQueue taskQueue,
       ILogger<BackgroundJob> logger,
       IHostApplicationLifetime applicationLifetime,
       JobQueue jobQueue)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _cancellationToken = applicationLifetime.ApplicationStopping;
            _jobQueue = jobQueue;
        }
        public void StartBackgroundWorkAsync()
        {
            _logger.LogInformation($"{nameof(QueueBackgroundWorkAsync)} Background is starting.");
            Task.Run(async () => await QueueBackgroundWorkAsync());
        }

        private async ValueTask QueueBackgroundWorkAsync()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                if (_jobQueue.SortJob.Any())
                {
                    // Enqueue a background work item

                    var itemToSort = _jobQueue.SortJob.First.Value;
                    await _taskQueue.QueueBackgroundWorkItemAsync(async (CancellationToken ct) =>
                    {
                        if (!ct.IsCancellationRequested)
                        {
                            _logger.LogInformation("Processing job with ID '{JobId}'.", itemToSort.Id);
                            var stopwatch = Stopwatch.StartNew();

                            var output = itemToSort.Input.OrderBy(n => n).ToArray();
                            await Task.Delay(5000); // NOTE: This is just to simulate a more expensive operation

                            var duration = stopwatch.Elapsed;

                            _logger.LogInformation("Completed processing job with ID '{JobId}'. Duration: '{Duration}'.", itemToSort.Id, duration);
                            itemToSort.Output = output;
                            itemToSort.Duration = duration;
                            itemToSort.Status = SortJobStatus.Completed;
                        }
                    });
                    _jobQueue.Dequeue();
                }
            }
        }
    }
}
