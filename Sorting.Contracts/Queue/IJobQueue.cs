﻿using Sorting.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.Contracts.Queue
{
    public interface IJobQueue
    {
        Task EnqueueJob<T>(T obj);
        Task Dequeue();
    }
}
