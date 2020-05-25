using AutoMapper;
using DutyFier.API.Entities;
using DutyFier.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyFier.API.Profiles
{
    public class DutyTypeProfile : Profile
    {
        public DutyTypeProfile()
        {
            CreateMap<DutyType, DutyTypeDto>()
                .ForMember(
                    dest => dest.CountPosition,
                    opt => opt.MapFrom(src => src.Positions.Count));

            CreateMap<DutyTypeForCreateDto, DutyType>();
        }
    }
}
