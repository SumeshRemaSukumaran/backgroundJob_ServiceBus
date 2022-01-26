using Maersk.Sorting.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maersk.Sorting.Contracts.DataService.Entities
{
    public interface IJobs
    {
        bool Add(Guid key, SortJobModel value);
        bool Update(Guid key, SortJobModel value);
        Task<SortJobModel> Get(Guid key);
        SortJobModel[] GetAll();
    }
}
