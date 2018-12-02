using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using AuctionZ.Models.Account;
using AutoMapper;

namespace AuctionZ.Models.MappingProfiles
{
    public class RegisterViewModelMappingProfile : Profile
    {
        public RegisterViewModelMappingProfile()
        {
            CreateMap<RegisterViewModel, UserDto>()
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.Money, opt => opt.Ignore())
                .ForMember(x => x.Bids, opt => opt.Ignore())
                .ForMember(x => x.Lots, opt => opt.Ignore());
            CreateMap<UserDto, RegisterViewModel>()
                .ForSourceMember(x => x.UserId, opt => opt.Ignore())
                .ForSourceMember(x => x.Money, opt => opt.Ignore())
                .ForSourceMember(x => x.Lots, opt => opt.Ignore())
                .ForSourceMember(x => x.Bids, opt => opt.Ignore());
        }
    }
}
