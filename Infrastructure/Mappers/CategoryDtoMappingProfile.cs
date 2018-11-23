using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace Infrastructure.Mappers
{
    public class CategoryDtoMappingProfile : Profile
    {
        public CategoryDtoMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}