using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ILotRepository : IRepository<Lot>
    {
        IEnumerable<Lot> FindWithBids(Expression<Func<Lot, bool>> predicate);
        IEnumerable<Lot> GetAllWithBids();
        IEnumerable<Lot> GetAllLotsForUser(int lotId);
        IEnumerable<Lot> GetAllLots(LotsFilterCriteria criteria);
        Lot GetByIdWithBids(int id);
        int GetLotsCount(LotsFilterCriteria criteria);
        IEnumerable<Lot> GetAllLotsWithUsers(LotsFilterCriteria criteria);
        IEnumerable<Lot> GetUserPurchases(int userId);
        IEnumerable<Lot> GetLotsByStatus(bool isFinished);
    }
}