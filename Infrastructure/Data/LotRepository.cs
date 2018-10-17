using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    class LotRepository : EfRepository<Lot>, ILotRepository
    {

        public LotRepository(AuctionContext context) : base(context)
        {

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


        public IEnumerable<Lot> FindWithBids(Func<Lot, bool> predicate)
        {
            return _context.Lots.Include(b => b.Bids).Where(predicate); // Todo: somehow swap Include and Where 
        }

    }
}
