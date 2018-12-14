using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class LotRepository : EfRepository<Lot>, ILotRepository
    {

        public LotRepository(AuctionContext context) : base(context) { }


        public IEnumerable<Lot> GetAllLotsForUser(int userId)
        {
            return _context.Lots.Where(x => x.UserId == userId);
        }


        private IQueryable<Lot> FilterByCriteria(LotsFilterCriteria criteria)
        {
            var lots = _context.Lots.AsQueryable();
            if (!string.IsNullOrEmpty(criteria.Title))
                lots = lots.Where(x => x.Name.Contains(criteria.Title));
            if (criteria.Category.HasValue)
                lots = lots.Where(x => x.CategoryId == criteria.Category);
            if (criteria.Active.HasValue) lots = criteria.Active.Value ? lots.Where(x => x.ExpirationTime > DateTime.Now) : lots.Where(x => x.ExpirationTime < DateTime.Now);
            if (criteria.UserId.HasValue)
                lots = lots.Where(x => x.UserId == criteria.UserId);
            return lots;
        }

        private IQueryable<Lot> GetLots(LotsFilterCriteria criteria)
        {
            var lots = FilterByCriteria(criteria);
            if (criteria.Page != 0)
                return lots.Skip((criteria.Page - 1) * criteria.PageSize).Take(criteria.PageSize);
            return lots;
        }


        public IEnumerable<Lot> GetAllLots(LotsFilterCriteria criteria) => GetLots(criteria);

        public IEnumerable<Lot> GetAllLotsWithUsers(LotsFilterCriteria criteria)
        {
            return GetLots(criteria).Include(x => x.User);
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

        public int GetLotsCount(LotsFilterCriteria criteria)
        {
            return FilterByCriteria(criteria).Count();
        }


        public Bid GetLastBidForLot(int lotId)
        {
            return _context.Bids //Todo change later
                .Where(b => b.LotId == lotId)
                .OrderByDescending(x => x.Price)
                .FirstOrDefault();
        }

        public IEnumerable<Lot> GetUserPurchases(int userId)
        {
            return _context.Lots
                .Where(x => x.ExpirationTime <= DateTime.Now && x.UserId != userId)
                .Where(x => x.Bids.OrderByDescending(y => y.Price).FirstOrDefault() != null &&
                           x.Bids.OrderByDescending(y => y.Price).FirstOrDefault().UserId == userId).Include(x => x.User);
        }

        public IEnumerable<Lot> GetLotsByStatus(bool isFinished)
        {
            return _context.Lots.Where(x => x.IsFinished==isFinished);
        }


    }
}
