using AutoMapper;
using Domain.Entities;

namespace IdentityService.Dtos
{
    public  class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}
