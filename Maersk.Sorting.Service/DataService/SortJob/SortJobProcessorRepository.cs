using Maersk.Sorting.Model.ViewModel;
using Maersk.Sorting.Service.Interface.DataService;
using Maersk.Sorting.Service.Interface.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maersk.Sorting.Service.DataService.SortJob
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

        public async Task<SortJobModel[]> GetJobs()
        {
            return await  _jobs.GetAll();
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
