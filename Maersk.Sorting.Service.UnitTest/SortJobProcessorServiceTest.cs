using AutoMapper;
using Maersk.Sorting.QueueService;
using Maersk.Sorting.Service.Interface.BusinessService.SortJob;
using Maersk.Sorting.Service.Interface.DataService;
using Maersk.Sorting.Service.SortJob;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Maersk.Sorting.BusinessService.UnitTest
{
    [TestClass]
    public class SortJobProcessorServiceTest
    {
        private ISortJobProcessorService _sortJobProcessorService;
        private readonly ILogger<SortJobProcessorService> _logger;
        private readonly ILogger<JobQueue> _loggerJobQueue;
        private readonly ISortJobProcessorRepository _sortJobProcessorRepository;
        private readonly IMapper _mapper;
        private readonly JobQueue _queueSortJob;
        public SortJobProcessorServiceTest()
        {
            _sortJobProcessorRepository = new Mock<ISortJobProcessorRepository>().Object;
            _logger = new Mock<ILogger<SortJobProcessorService>>().Object;
            _mapper = new Mock<IMapper>().Object;
            _loggerJobQueue = new Mock<ILogger<JobQueue>>().Object;
            _queueSortJob = new JobQueue(_loggerJobQueue);

            _sortJobProcessorService = new SortJobProcessorService(
                _logger,
                _sortJobProcessorRepository,
                _mapper,
                _queueSortJob);
        }
        [TestMethod]
        public void SortJobProcessorService_EnqueueJob_Expected()
        {
            var job = EnqueueJob();
            Assert.IsNotNull(job);
        }

        [TestMethod]
        public void SortJobProcessorService_GetJob_Expected()
        {
            Guid jobId = EnqueueJob();
            var job = _sortJobProcessorService.GetJob(jobId);
            Assert.IsNotNull(job);
        }

        [TestMethod]
        public void SortJobProcessorService_GetJobs_Expected()
        {
            EnqueueJob();
            var jobs = _sortJobProcessorService.GetJobs();
            Assert.IsNotNull(jobs.Result);
        }

        private Guid EnqueueJob()
        {
            int[] values = { 10, 3, 2, -1 };
            var job = _sortJobProcessorService.EnqueueJob(values);
            return job.Id;
        }
    }
}
