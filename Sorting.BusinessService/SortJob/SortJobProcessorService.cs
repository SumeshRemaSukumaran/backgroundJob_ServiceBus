using AutoMapper;
using Sorting.Contracts.BusinessService.SortJob;
using Sorting.Contracts.DataService;
using Sorting.Contracts.Queue;
using Sorting.Model.Dto;
using Sorting.Model.Enum;
using Sorting.Model.ViewModel;
using Sorting.QueueService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sorting.BusinessService
{
    public class SortJobProcessorService : ISortJobProcessorService
    {
        private readonly ILogger<SortJobProcessorService> _logger;
        private readonly ISortJobProcessorRepository _sortJobProcessorRepository;
        private readonly IJobQueue _queueSortJob;
        private readonly IMapper _mapper;

        public SortJobProcessorService(ILogger<SortJobProcessorService> logger,
            ISortJobProcessorRepository sortJobProcessorRepository,
            IMapper mapper,
            IJobQueue queueSortJob)
        {
            _logger = logger;
            _sortJobProcessorRepository = sortJobProcessorRepository;
            _mapper = mapper;
            _queueSortJob = queueSortJob;
        }

        public SortJobModel EnqueueJob(int[] values)
        {
            SortJobModel sortJob = new SortJobModel
            {
                Id = Guid.NewGuid(),
                Status = SortJobStatus.Pending,
                Duration = null,
                Input = values,
                Output = null,
            };
            SaveJob(sortJob);
            _queueSortJob.EnqueueJob(sortJob);
            return sortJob;
        }

        public async Task<SortJobModel> GetJob(System.Guid jobId)
        {
            return await _sortJobProcessorRepository.GetJob(jobId);
        }

        public async Task<SortJobModel[]> GetJobs()
        {
            return  _sortJobProcessorRepository.GetJobs();
        }

        public async Task<SortJobModel> Process(SortJobModel job)
        {
            _logger.LogInformation("Processing job with ID '{JobId}'.", job.Id);
            var stopwatch = Stopwatch.StartNew();

            var output = job.Input.OrderBy(n => n).ToArray();
            await Task.Delay(5000); // NOTE: This is just to simulate a more expensive operation

            var duration = stopwatch.Elapsed;

            _logger.LogInformation("Completed processing job with ID '{JobId}'. Duration: '{Duration}'.", job.Id, duration);

            job.Output = output;
            job.Duration = duration;
            job.Status = SortJobStatus.Completed;
            return job;
        }
        private void SaveJob(SortJobModel jobModel)
        {
            _sortJobProcessorRepository.SaveJob(jobModel);
        }
    }
}
