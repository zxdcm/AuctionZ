using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace Infrastructure.Mappers
{
    class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile() => CreateMap<User, UserDto>().ReverseMap();
    }
}
