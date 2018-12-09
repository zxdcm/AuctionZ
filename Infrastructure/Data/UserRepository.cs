using System;
using System.Linq;
using System.Transactions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {

        public UserRepository(AuctionContext dbContext) : base(dbContext)
        {}


        public void MakeBid(int lotId, int userId, decimal bidValue)
        {
            using (var transaction = new TransactionScope())
            {
                var lot = _context.Lots.First(x => x.LotId == lotId);
                var user = _context.Users.First(x => x.UserId == userId);

                var lastBid =  _context.Bids 
                    .Where(b => b.LotId == lotId)
                    .OrderByDescending(x => x.Price)
                    .FirstOrDefault();

                if (lastBid != null)
                {
                    var lastBidder = _context.Users.First(x => x.UserId == lastBid.UserId);
                    lastBidder.Money += lastBid.Price;
                }

                user.Money -= bidValue;

                _context.Bids.Add(new Bid()
                {
                    LotId = lot.LotId,
                    DateOfBid = DateTime.Now,
                    Price = bidValue,
                    User = user
                });
                lot.Price = bidValue;

                _context.SaveChanges();

                transaction.Complete();
            }
        }

    }
}