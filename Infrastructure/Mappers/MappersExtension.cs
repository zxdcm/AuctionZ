using System.Collections.Generic;
using System.Linq;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace Infrastructure.Mappers
{
    public static class MappingExtension
    {
      

        #region LotMappers
        public static Lot ToDal(this LotDto dto) => Mapper.Map<Lot>(dto);
        public static LotDto ToDto(this Lot entity) => Mapper.Map<LotDto>(entity);

        public static IEnumerable<Lot> ToDal(this IEnumerable<LotDto> dto) 
            => Mapper.Map<IEnumerable<Lot>>(dto);
        public static IEnumerable<LotDto> ToDto(this IEnumerable<Lot> entity) 
            => Mapper.Map<IEnumerable<LotDto>>(entity);


        #endregion

        #region BidMappers

        public static Bid ToDal(this BidDto dto) => Mapper.Map<Bid>(dto);
        public static BidDto ToDto(this Bid entity) => Mapper.Map<BidDto>(entity);

        public static IEnumerable<Bid> ToDal(this IEnumerable<BidDto> dto) 
            => Mapper.Map<IEnumerable<Bid>>(dto);
        public static IEnumerable<BidDto> ToDto(this IEnumerable<Bid> entity) => 
            Mapper.Map<IEnumerable<BidDto>>(entity);


        #endregion

        #region UserMappers

        public static User ToDal(this UserDto dto) => Mapper.Map<User>(dto);
        public static UserDto ToDto(this User entity) => Mapper.Map<UserDto>(entity);

        public static IEnumerable<User> ToDal(this IEnumerable<UserDto> dto) => 
            Mapper.Map<IEnumerable<User>>(dto);
        public static IEnumerable<UserDto> ToDto(this IEnumerable<User> entity) => 
            Mapper.Map<IEnumerable<UserDto>>(entity);

        #endregion

        #region CategoryMappers


        public static Category ToDal(this CategoryDto category) => Mapper.Map<Category>(category);

        public static CategoryDto ToDto(this Category category) => Mapper.Map<CategoryDto>(category);

        #endregion

    }
}