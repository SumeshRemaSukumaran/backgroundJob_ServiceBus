using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting.Model.Enum;
using Sorting.Model.ViewModel;

namespace Sorting.Contracts.DataService
{
    public interface ISortJobProcessorRepository
    {
        SortJobModel[] GetJobs();
        Task<SortJobModel> GetJob(Guid key);
        bool SaveJob(SortJobModel sortJobModel);
        bool UpdateJob(SortJobModel sortJobModel);
    }
}
