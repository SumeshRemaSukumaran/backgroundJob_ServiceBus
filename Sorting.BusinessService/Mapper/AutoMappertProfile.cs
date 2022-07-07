using AutoMapper;
using Sorting.Model.Dto;
using Sorting.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting.BusinessService.Mapper
{
    public class AutoMappertProfile : Profile
    {
        public AutoMappertProfile()
        {
            CreateMap<SortJobModel, SortJobDto>().ReverseMap();
        }
    }
}
