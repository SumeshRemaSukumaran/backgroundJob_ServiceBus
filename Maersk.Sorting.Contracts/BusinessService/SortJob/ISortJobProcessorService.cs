using Maersk.Sorting.Model.Dto;
using Maersk.Sorting.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace Maersk.Sorting.Contracts.BusinessService.SortJob
{
    public interface ISortJobProcessorService
    {
        Task<SortJobModel> Process(SortJobModel job);
        SortJobModel EnqueueJob(int[] values);
        Task<SortJobModel[]> GetJobs();
        Task<SortJobModel> GetJob(Guid jobId);
    }
}