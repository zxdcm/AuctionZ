using System;
using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ILotRepository : IRepository<Lot>
    {
        Lot GetByIdWithBids(int id);
        IEnumerable<Lot> GetAllWithBids();
        IEnumerable<Lot> GetAllByCategory(Func<Category, bool> predicate);
    }
}