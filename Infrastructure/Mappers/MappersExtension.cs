using System.Collections.Generic;
using System.Linq;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace Infrastructure.Mappers
{
    public static class MappingExtension
    {
//
//        public static BidDto ToDto(this Bid bid) =>
//            ToDtoWithCache(bid, new Dictionary<object, object>());
//
//        static BidDto ToDtoWithCache(Bid bid, Dictionary<object, object> cache)
//        {
//            if (cache.TryGetValue(bid, out var result))
//                return (BidDto)result;
//
//            var dto = new BidDto();
//            cache[bid] = dto;
//
//            dto.User = ToDtoWithCache(bid.User, cache);
//
//            return dto;
//        }
//
//        public static UserDto ToDto(this User user) =>
//            ToDtoWithCache(user, new Dictionary<object, object>());
//
//        static UserDto ToDtoWithCache(User user, Dictionary<object, object> cache)
//        {
//            if (cache.TryGetValue(user, out var result))
//                return (UserDto)result;
//
//            var dto = new UserDto();
//            cache[user] = dto;
//
//            dto.Bids = user.Bids?.Select(x => ToDtoWithCache(x, cache)).ToList();
//
//            return dto;
//        }



        #region LotMappers
        public static Lot ToDal(this LotDto lot)
        {
//            return new Lot()
//            {
//                LotId = lot.LotId,
//                Name = lot.Name,
//                Description = lot.Description,
//                Price = lot.Price,
//                ExpirationTime = lot.ExpirationTime,
//                CategoryId = lot.CategoryId,
//                Category = lot.Category?.ToDal(),
//                Bids = lot.Bids?.Select(x => x.ToDal()).ToList(),
//                UserId = lot.UserId
//            };
            return Mapper.Map<Lot>(lot);
        }

        public static LotDto ToDto(this Lot lot)
        {
//            return new LotDto()
//            {
//                LotId = lot.LotId,
//                Name = lot.Name,
//                Description = lot.Description,
//                Price = lot.Price,
//                ExpirationTime = lot.ExpirationTime,
//                CategoryId = lot.CategoryId,
//                Category = lot.Category?.ToDto(),
//                Bids = lot.Bids?.Select(x => x.ToDto()).ToList(),
//                UserId = lot.UserId
//            };
            return Mapper.Map<LotDto>(lot);
        }

        #endregion

        #region BidMappers

        public static Bid ToDal(this BidDto bid)
        {
//            return new Bid()
//            {
//                BidId = bid.BidId,
//                LotId = bid.LotId,
//                UserId = bid.UserId,
//                User = bid.User?.ToDal(),
//                DateOfBid = bid.DateOfBid,
//                Price = bid.Price,
//                Lot = bid.Lot?.ToDal(),
//            };
             return Mapper.Map<Bid>(bid);
        }

        public static BidDto ToDto(this Bid bid)
        {
//            return new BidDto()
//            {
//                BidId = bid.BidId,
//                LotId = bid.LotId,
//                UserId = bid.UserId,
//                User = bid.User?.ToDto(),
//                DateOfBid = bid.DateOfBid,
//                Price = bid.Price,
//                Lot = bid.Lot?.ToDto(),
//            };
            return Mapper.Map<BidDto>(bid);
        }
        #endregion

        #region UserMappers

        public static User ToDal(this UserDto user)
        {
//            return new User()
//            {
//                UserId = user.UserId,
//                Money = user.Money,
//                Name = user.Name,
//                Bids = user.Bids?.Select(x => x.ToDal()).ToList(),
//                Lots = user.Lots?.Select(x => x.ToDal()).ToList(),
//            };
            return Mapper.Map<User>(user);
        }

        public static UserDto ToDto(this User user)
        {
            //            return new UserDto()
            //            {
            //                UserId = user.UserId,
            //                Money = user.Money,
            //                Name = user.Name,
            //                Bids = user.Bids?.Select(x => x.ToDto()).ToList(),
            //                Lots = user.Lots?.Select(x => x.ToDto()).ToList(),
            //            };
            return Mapper.Map<UserDto>(user);
        }

        #endregion

        #region CategoryMappers


        public static Category ToDal(this CategoryDto category)
        {
//            return new Category()
//            {
//                CategoryId = category.CategoryId,
//                Name = category.Name,
//                Lots = category.Lots?.Select(x => x.ToDal()).ToList()
//            };
            return Mapper.Map<Category>(category);
        }

        public static CategoryDto ToDto(this Category category)
        {
//            return new CategoryDto()
//            {
//                CategoryId = category.CategoryId,
//                Name = category.Name,
//                Lots = category.Lots?.Select(x => x.ToDto()).ToList()
//            };
            return Mapper.Map<CategoryDto>(category);
        }



        #endregion

    }
}