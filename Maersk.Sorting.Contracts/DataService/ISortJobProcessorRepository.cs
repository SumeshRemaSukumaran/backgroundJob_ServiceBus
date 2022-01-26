using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maersk.Sorting.Model.Enum;
using Maersk.Sorting.Model.ViewModel;

namespace Maersk.Sorting.Contracts.DataService
{
    public interface ISortJobProcessorRepository
    {
        SortJobModel[] GetJobs();
        Task<SortJobModel> GetJob(Guid key);
        bool SaveJob(SortJobModel sortJobModel);
        bool UpdateJob(SortJobModel sortJobModel);
    }
}
