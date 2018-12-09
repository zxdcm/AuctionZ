using System.Collections.Generic;
using ApplicationCore.DTO;
using AuctionZ.Models.Account;
using AutoMapper;


namespace AuctionZ.Models.MappingProfiles
{
    public static class MappersExtension
    {
        public static LotDto ToDto(this LotViewModel vm) => Mapper.Map<LotDto>(vm);
        public static LotViewModel ToVm(this LotDto dto) => Mapper.Map<LotViewModel>(dto);
    
        public static IEnumerable<LotDto> ToDto(this IEnumerable<LotViewModel> vm) 
            => Mapper.Map<IEnumerable<LotDto>>(vm);
        public static IEnumerable<LotViewModel> ToVm(this IEnumerable<LotDto> dto) 
            => Mapper.Map<IEnumerable<LotViewModel>>(dto);


        public static BidDto ToDto(this BidViewModel vm) => Mapper.Map<BidDto>(vm);
        public static BidViewModel ToVm(this BidDto dto) => Mapper.Map<BidViewModel>(dto);

        public static IEnumerable<BidDto> ToDto(this IEnumerable<BidViewModel> vm)
            => Mapper.Map<IEnumerable<BidDto>>(vm);
        public static IEnumerable<BidViewModel> ToVm(this IEnumerable<BidDto> dto)
            => Mapper.Map<IEnumerable<BidViewModel>>(dto);

        public static UserDto ToDto(this RegisterViewModel vm)
            => Mapper.Map<UserDto>(vm);

        public static UserDto ToDto(this ProfileViewModel vm)
            => Mapper.Map<UserDto>(vm);
        public static ProfileViewModel ToVm(this UserDto vm) 
            => Mapper.Map<ProfileViewModel>(vm);

    }
}
