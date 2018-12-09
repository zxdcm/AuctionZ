using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ApplicationCore;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Mappers;

namespace Infrastructure.Services
{
    public class LotsService : ILotsService
    {

        private readonly ILotRepository _lotRepository; 

        public LotsService(ILotRepository lotRepository)
        {
            this._lotRepository = lotRepository;
        }

        public LotDto GetItem(int id)
        {
            return _lotRepository.GetById(id)?.ToDto();
        }

        public IEnumerable<LotDto> GetItems()
        {
            return _lotRepository.ListAll().ToDto();
        }

        public IEnumerable<LotDto> Find(Expression<Func<LotDto, bool>> predicate)
        {
            throw new NotImplementedException();
            // return _lotRepository.Find(predicate);
        }

        public LotDto AddItem(LotDto lot)
        {
            return _lotRepository.Create(lot.ToDal()).ToDto();
        }

        public void Update(LotDto lot)
        {
            var orm = _lotRepository.GetById(lot.LotId);
            orm = lot.ToDal(orm);
            _lotRepository.Update(orm);
        }

        public void RemoveItem(int id)
        {
            Lot lot = _lotRepository.GetById(id);
            _lotRepository.Delete(lot);
        }

        public IEnumerable<LotDto> GetAllLots(LotsFilterCriteria criteria)
        {
            if (criteria.Category == 0) // Skip default;
                criteria.Category = null;
            return _lotRepository.GetAllLots(criteria).ToDto();
        }

        public LotDto GetByIdWithBids(int id)
        {
            return _lotRepository.GetByIdWithBids(id).ToDto();
        }

        public IEnumerable<LotDto> GetAllWithBids()
        {
            return _lotRepository.GetAllWithBids().ToDto();
        }

        public IEnumerable<LotDto> FindWidthBids(Expression<Func<LotDto, bool>> predicate)
        {
            throw new NotImplementedException();;
            //return _lotRepository.FindWithBids(predicate);
        }

        public IEnumerable<LotDto> GetAllLotsForUser(int lotId)
        {
            return _lotRepository.GetAllLotsForUser(lotId).ToDto();
        }

        public int GetLotsCount(LotsFilterCriteria criteria)
        {
            return _lotRepository.GetLotsCount(criteria);
        }

        public IEnumerable<LotDto> GetAllLotsWithUsers(LotsFilterCriteria criteria)
        {
            if (criteria.Category == 0) // Skip default;
                criteria.Category = null;
            return _lotRepository.GetAllLotsWithUsers(criteria).ToDto();
        }

        public IEnumerable<LotDto> GetUserPurchases(int userId)
        {
            return _lotRepository.GetUserPurchases(userId).ToDto();
        }

    }
      
   
}