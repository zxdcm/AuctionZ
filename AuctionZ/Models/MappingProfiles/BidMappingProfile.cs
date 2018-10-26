using ApplicationCore.Entities;
using AutoMapper;

namespace AuctionZ.Models.MappingProfiles
{
    public class BidMappingProfile : Profile
    {
        public BidMappingProfile()
        {
            CreateMap<Bid, BidViewModel>();
            CreateMap<BidViewModel, Bid>();
        }
    }
}