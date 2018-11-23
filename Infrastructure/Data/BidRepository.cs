using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BidRepository : EfRepository<Bid>, IBidRepository
    {
        public BidRepository(AuctionContext context) : base(context)
        {

        }

        public Bid GetLastBidForLot(int lotId)
        {
            return _context.Bids //Todo change later
                .Where(b => b.LotId == lotId)
                .OrderByDescending(x => x.Price)
                .FirstOrDefault();
        }

        public IEnumerable<Bid> GetAllBidsForLotWithUsers(int lotId)
        {
            return _context.Bids
                .Where(x => x.LotId == lotId)
                .Include(x => x.User);
        }

        public IEnumerable<Bid> GetAllBidsForUser(int userId)
        {
            return _context.Bids.Where(x => x.UserId == userId);
        }
    }
}