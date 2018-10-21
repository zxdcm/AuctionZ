using System;
using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ILotsService : IManagementService<Lot>
    {
        IEnumerable<Lot> Find(Func<Lot, bool> predicate);
        IEnumerable<Lot> FindWidthBids(Func<Lot, bool> predicate);
        IEnumerable<Lot> GetAllWithBids();
        Lot GetByIdWithBids(int id);
    }
}