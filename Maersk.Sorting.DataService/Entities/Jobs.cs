using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maersk.Sorting.Contracts.DataService.Entities;
using Maersk.Sorting.Model.ViewModel;

namespace Maersk.Sorting.DataService.Entities
{
    public class Jobs : IJobs
    {
        private readonly ConcurrentDictionary<Guid, SortJobModel> _jobs;

        public Jobs()
        {
            _jobs = new ConcurrentDictionary<Guid, SortJobModel>();
        }

        public bool Add(Guid key, SortJobModel value)
        {
            return _jobs.TryAdd(key, value);
        }

        public Task<SortJobModel> Get(Guid key)
        {
            _jobs.TryGetValue(key, out SortJobModel job);
            return Task.FromResult(job);
        }

        public SortJobModel[] GetAll()
        {
            var keys = _jobs.Keys.ToList();
            SortJobModel[] jobs = new SortJobModel[keys.Count];
            int i = 0;
            foreach (var key in keys)
            {
                _jobs.TryGetValue(key, out SortJobModel job);
                jobs[i] = job;
                i++;
            }
            return jobs;
        }

        public bool Update(Guid key, SortJobModel value)
        {
            return _jobs.TryUpdate(key, value, _jobs[key]);
        }
    }
}
