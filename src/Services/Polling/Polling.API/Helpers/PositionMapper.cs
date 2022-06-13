using AutoMapper;
using Polling.API.Dtos;
using Polling.Domain.Entities;
using System.Collections.Generic;

namespace Polling.API.Helpers
{
    public class PositionMapper : Profile
    {
        public PositionMapper()
        {
            CreateMap<EntityPosition, PositionModel>().ReverseMap();
            CreateMap<EntityPosition, CreatePositionModel>().ReverseMap();
        }
    }
}
