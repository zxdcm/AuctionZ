using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace Infrastructure.Mappers
{
    public class RoleDtoMappingProfile : Profile
    {
        public RoleDtoMappingProfile()
        {
            CreateMap<RoleDto, Role>().ReverseMap();
        }
    }
}