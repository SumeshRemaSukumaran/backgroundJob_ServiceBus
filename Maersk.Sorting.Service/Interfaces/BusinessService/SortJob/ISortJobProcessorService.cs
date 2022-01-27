using Maersk.Sorting.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace Maersk.Sorting.Service.Interface.BusinessService.SortJob
{
    public interface ISortJobProcessorService
    {
        Task<SortJobModel> Process(SortJobModel job);
        SortJobModel EnqueueJob(int[] values);
        Task<SortJobModel[]> GetJobs();
        Task<SortJobModel> GetJob(Guid jobId);
    }
}