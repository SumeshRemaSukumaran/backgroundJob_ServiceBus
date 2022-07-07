using Sorting.Contracts.DataService;
using Sorting.Contracts.DataService.Entities;
using Sorting.Model.Enum;
using Sorting.Model.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.DataService.SortJob
{
    public class SortJobProcessorRepository : ISortJobProcessorRepository
    {
        private readonly IJobs _jobs;
        public SortJobProcessorRepository(IJobs jobs)
        {
            _jobs = jobs;
        }

        public async Task<SortJobModel> GetJob(Guid key)
        {
            SortJobModel job = await _jobs.Get(key);
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
