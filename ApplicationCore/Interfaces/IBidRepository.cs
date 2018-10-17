using System;
using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBidRepository : IRepository<Bid>
    {
        Bid GetLastBidForLot(Lot lot);
        IEnumerable<Bid> GetAllBidsForLot(Func<Lot, bool> predicate);
        IEnumerable<Bid> GetAllBidsForUser(Func<Lot, bool> predicate);
    }
}