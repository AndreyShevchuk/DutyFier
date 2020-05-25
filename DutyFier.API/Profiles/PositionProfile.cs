using AutoMapper;
using DutyFier.API.Entities;
using DutyFier.API.Models;

namespace DutyFier.API.Profiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionDto>();
            CreateMap<PositionForCreateDto, Position>();
        }
    }
}
