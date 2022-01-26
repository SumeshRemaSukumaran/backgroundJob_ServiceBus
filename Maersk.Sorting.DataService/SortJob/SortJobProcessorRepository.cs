using Maersk.Sorting.Contracts.DataService;
using Maersk.Sorting.Contracts.DataService.Store;
using Maersk.Sorting.Model.Enum;
using Maersk.Sorting.Model.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maersk.Sorting.DataService.SortJob
{
    public class SortJobProcessorRepository : ISortJobProcessorRepository
    {
        private readonly IJobs _jobs;
        public SortJobProcessorRepository(IJobs jobs)
        {
            _jobs = jobs;
        }

        public SortJobModel GetJob(Guid key)
        {
            (bool isExists, SortJobModel job) = _jobs.Get(key);
            return job;
        }

        public SortJobModel[] GetJobs()
        {
            return _jobs.GetAll();
        }

        public bool SaveJob(SortJobModel sortJobModel)
        {
            return _jobs.Add(sortJobModel.Id, sortJobModel);
        }

        public bool UpdateJob(SortJobModel sortJobModel)
        {
            return _jobs.Update(sortJobModel.Id, sortJobModel);
        }
    }
}
