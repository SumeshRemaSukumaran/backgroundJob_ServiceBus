using Maersk.Sorting.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace Maersk.Sorting.Service.Interface.Entities
{
    public interface IJobs
    {
        bool Add(Guid key, SortJobModel value);
        bool Update(Guid key, SortJobModel value);
        Task<SortJobModel> Get(Guid key);
        Task<SortJobModel[]> GetAll();
    }
}
