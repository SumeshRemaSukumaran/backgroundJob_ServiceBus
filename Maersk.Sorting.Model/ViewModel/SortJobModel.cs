using Maersk.Sorting.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maersk.Sorting.Model.ViewModel
{
    public class SortJobModel
    {
        public Guid Id { get; set; }
        public SortJobStatus Status { get; set; }
        public TimeSpan? Duration { get; set; }
        public ICollection<int> Input { get; set; }
        public ICollection<int>? Output { get; set; }
    }
}
