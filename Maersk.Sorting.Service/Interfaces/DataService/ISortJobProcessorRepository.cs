using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maersk.Sorting.Model.ViewModel;

namespace Maersk.Sorting.Service.Interface.DataService
{
    public interface ISortJobProcessorRepository
    {
        Task<SortJobModel[]> GetJobs();
        Task<SortJobModel> GetJob(Guid key);
        bool SaveJob(SortJobModel sortJobModel);
        bool UpdateJob(SortJobModel sortJobModel);
    }
}
