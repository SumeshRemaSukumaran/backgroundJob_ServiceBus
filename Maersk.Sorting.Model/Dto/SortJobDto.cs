using Maersk.Sorting.Model.Enum;
using System;
using System.Collections.Generic;

namespace Maersk.Sorting.Model.Dto
{
    public class SortJobDto
    {
        public Guid Id { get; set; }
        public SortJobStatus Status { get; set; }
        public TimeSpan? Duration { get; set; }
        public ICollection<int> Input { get; set; }
        public ICollection<int>? Output { get; set; }
    }
}
