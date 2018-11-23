using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ApplicationCore.DTO;


namespace ApplicationCore.Interfaces
{
    public interface IBidsService : IManagementService<BidDto>
    {
        IEnumerable<BidDto> Find(Expression<Func<BidDto, bool>> predicate);
        IEnumerable<BidDto> GetAllBidsForLotWithUsers(int lotId);
        IEnumerable<BidDto> GetAllBidsForUser(int userId);
        BidDto GetLastBidForLot(int lotId);
    }
}