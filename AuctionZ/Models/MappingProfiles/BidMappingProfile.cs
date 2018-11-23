using ApplicationCore.DTO;
using AutoMapper;

namespace AuctionZ.Models.MappingProfiles
{
    public class BidMappingProfile : Profile
    {
        public BidMappingProfile()
        {
            CreateMap<BidDto, BidViewModel>();
            CreateMap<BidViewModel, BidDto>();
        }
    }
}