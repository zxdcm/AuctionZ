using ApplicationCore.Entities;
using AutoMapper;

namespace AuctionZ.Models.MappingProfiles
{
    public class LotMappingProfile : Profile
    {
        public LotMappingProfile()
        {
            CreateMap<Lot, LotViewModel>()
                .ForMember(x => x.Categories, opt => opt.Ignore());
            CreateMap<LotViewModel, Lot>();
        }
    }
}