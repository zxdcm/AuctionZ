using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class LotRepository : EfRepository<Lot>, ILotRepository
    {

        public LotRepository(AuctionContext context) : base(context)
        {

        }


        public IEnumerable<Lot> GetAllLotsForUser(int userId)
        {
            return _context.Lots.Where(x => x.UserId == userId);
        }

        public Lot GetByIdWithBids(int id)
        {
            return _context.Lots
                .Where(x => x.LotId == id)
                .Include(b => b.Bids)
                .FirstOrDefault();
        }

        public IEnumerable<Lot> GetAllWithBids()
        {
            return _context.Lots.Include(b => b.Bids);
        }


        public IEnumerable<Lot> FindWithBids(Expression<Func<Lot, bool>> predicate)
        {
            return _context.Lots.Include(b => b.Bids).Where(predicate); // Todo: somehow swap Include and Where 
        }

    }
}
