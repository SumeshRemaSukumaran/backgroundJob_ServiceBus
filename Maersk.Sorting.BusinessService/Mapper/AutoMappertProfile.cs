using AutoMapper;
using Maersk.Sorting.Model.Dto;
using Maersk.Sorting.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maersk.Sorting.BusinessService.Mapper
{
    public class AutoMappertProfile : Profile
    {
        public AutoMappertProfile()
        {
            CreateMap<SortJobModel, SortJobDto>().ReverseMap();
        }
    }
}
