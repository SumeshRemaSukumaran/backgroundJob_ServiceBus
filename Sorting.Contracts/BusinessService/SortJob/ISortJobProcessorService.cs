using Sorting.Model.Dto;
using Sorting.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace Sorting.Contracts.BusinessService.SortJob
{
    public interface ISortJobProcessorService
    {
        Task<SortJobModel> Process(SortJobModel job);
        SortJobModel EnqueueJob(int[] values);
        Task<SortJobModel[]> GetJobs();
        Task<SortJobModel> GetJob(Guid jobId);
    }
}