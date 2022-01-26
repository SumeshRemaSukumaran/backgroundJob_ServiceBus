﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maersk.Sorting.Contracts.DataService.Store;
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

        public (bool, SortJobModel) Get(Guid key)
        {
            return (_jobs.TryGetValue(key, out SortJobModel job), job);
        }

        public SortJobModel[] GetAll()
        {
            var keys = _jobs.Keys.ToList();
            SortJobModel[] jobs =new SortJobModel[keys.Count];
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
