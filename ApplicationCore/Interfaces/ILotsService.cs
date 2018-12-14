using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ApplicationCore.DTO;

namespace ApplicationCore.Interfaces
{
    public interface ILotsService : IManagementService<LotDto>
    {
        IEnumerable<LotDto> Find(Expression<Func<LotDto, bool>> predicate);
        IEnumerable<LotDto> FindWidthBids(Expression<Func<LotDto, bool>> predicate);
        IEnumerable<LotDto> GetAllWithBids();
        IEnumerable<LotDto> GetAllLotsForUser(int userId);
        IEnumerable<LotDto> GetAllLots(LotsFilterCriteria criteria);
        LotDto GetByIdWithBids(int id);
        int GetLotsCount(LotsFilterCriteria criteria);
        IEnumerable<LotDto> GetAllLotsWithUsers(LotsFilterCriteria criteria);
        IEnumerable<LotDto> GetUserPurchases(int userId);
        IEnumerable<LotDto> GetLotsByStatus(bool isFinished);

    }
}