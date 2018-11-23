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
        Lot GetByIdWithBids(int id);
    }
}