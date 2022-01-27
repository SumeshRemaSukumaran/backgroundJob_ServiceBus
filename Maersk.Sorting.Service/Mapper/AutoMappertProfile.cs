using AutoMapper;
using Maersk.Sorting.Model.ViewModel;
using Maersk.Sorting.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maersk.Sorting.Service.Mapper
{
    public class AutoMappertProfile : Profile
    {
        public AutoMappertProfile()
        {
            CreateMap<SortJobModel, SortJobDto>().ReverseMap();
        }
    }
}
