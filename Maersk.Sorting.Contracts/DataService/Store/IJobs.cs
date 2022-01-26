using Maersk.Sorting.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maersk.Sorting.Contracts.DataService.Store
{
  public  interface IJobs
    {
        bool Add(Guid key, SortJobModel value);
        bool Update(Guid key, SortJobModel value);
        (bool, SortJobModel) Get(Guid key);
        SortJobModel[] GetAll();
    }
}
