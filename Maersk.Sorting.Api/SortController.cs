namespace Maersk.Sorting.Api.Controllers
{
    using AutoMapper;
    using Maersk.Sorting.Contracts.BusinessService.SortJob;
    using Maersk.Sorting.Model.Dto;
    using Maersk.Sorting.Model.Enum;
    using Maersk.Sorting.Model.ViewModel;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="SortController" />.
    /// </summary>
    [ApiController]
    [Route("sort")]
    public class SortController : ControllerBase
    {
        /// <summary>
        /// Defines the _sortJobProcessor.
        /// </summary>
        private readonly ISortJobProcessorService _sortJobProcessor;

        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortController"/> class.
        /// </summary>
        /// <param name="sortJobProcessor">The sortJobProcessor<see cref="ISortJobProcessorService"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public SortController(ISortJobProcessorService sortJobProcessor, IMapper mapper)
        {
            _sortJobProcessor = sortJobProcessor;
            _mapper = mapper;
        }

        /// <summary>
        /// The EnqueueAndRunJob.
        /// </summary>
        /// <see cref="EnqueueAndRunJob(int[])"/>
        /// <returns>The <see cref="ActionResult{SortJobDto}"/>.</returns>
        [HttpPost("run")]
        [Obsolete("This executes the sort job asynchronously. Use the asynchronous 'EnqueueJob' instead.")]
        public async Task<ActionResult<SortJobDto>> EnqueueAndRunJob(int[] values)
        {
            var pendingJob = new SortJobDto
            {
                Id = Guid.NewGuid(),
                Status = SortJobStatus.Pending,
                Duration = null,
                Input = values,
            };
            var sortJob = _mapper.Map<SortJobModel>(pendingJob);
            var completedJob = await _sortJobProcessor.Process(sortJob);
            return Ok(_mapper.Map<SortJobDto>(completedJob));
        }

        /// <summary>
        /// Enqueue a job to be processed in the background.
        /// </summary>
        ///<see cref="EnqueueJob(int[])"/>
        /// <returns>The <see cref="ActionResult{SortJobDto}"/>.</returns>
        [HttpPost]
        public ActionResult<SortJobDto> EnqueueJob(int[] values)
        {
            var jobModel = _sortJobProcessor.EnqueueJob(values);
            var jobDto = _mapper.Map<SortJobDto>(jobModel);
            return Ok(jobDto);
        }

        /// <summary>
        /// Gets all jobs that have been enqueued (both pending and completed).
        /// </summary>
        /// <returns>The <see cref="ActionResult{SortJobDto}"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<SortJobDto[]>> GetJobs()
        {
            var jobs = await _sortJobProcessor.GetJobs();
            var jobDto = _mapper.Map<SortJobDto[]>(jobs);
            return Ok(jobDto);
        }

        /// <summary>
        /// Get a specific job by ID.
        /// </summary>
        /// <param name="jobId">The jobId<see cref="Guid"/>.</param>
        /// <returns>The <see cref="ActionResult{SortJobDto}"/>.</returns>
        [HttpGet("{jobId}")]
        public async Task<ActionResult<SortJobDto>> GetJob(Guid jobId)
        {
            var job = await _sortJobProcessor.GetJob(jobId);
            var jobDto = _mapper.Map<SortJobDto>(job);
            return Ok(jobDto);
        }
    }
}
