using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace Infrastructure.Mappers
{
    public class BidDtoMappingProfile : Profile
    {
        public BidDtoMappingProfile() => CreateMap<Bid, BidDto>().ReverseMap();
    }
}