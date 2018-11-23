using ApplicationCore.DTO;
using AutoMapper;

namespace AuctionZ.Models.MappingProfiles
{
    public class LotMappingProfile : Profile
    {
        public LotMappingProfile()
        {
            CreateMap<LotDto, LotViewModel>()
                .ForMember(x => x.Categories, opt => opt.Ignore())
                .ForMember(x => x.ImageFile, opt => opt.Ignore());
            CreateMap<LotViewModel, LotDto>();
        }
    }
}