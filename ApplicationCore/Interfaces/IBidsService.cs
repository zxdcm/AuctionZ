using System;
using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBidsService : IManagementService<Bid>
    {
        IEnumerable<Bid> Find(Func<Bid, bool> predicate);
        IEnumerable<Bid> GetAllBidsForLotWithUsers(int lotId);
        IEnumerable<Bid> GetAllBidsForUser(int userId);
        Bid GetLastBidForLot(int lotId);
    }
}