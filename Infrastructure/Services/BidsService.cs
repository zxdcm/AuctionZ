using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Mappers;

namespace Infrastructure.Services
{
    public class BidsService : IBidsService
    {
        private readonly IBidRepository _bidRepository;

        public BidsService(IBidRepository bidRepository)
        {
            this._bidRepository = bidRepository;

        }

        public BidDto GetItem(int id)
        {
            return _bidRepository.GetById(id)?.ToDto();
        }

        public IEnumerable<BidDto> GetItems()
        {
            return _bidRepository.ListAll().ToDto();
        }

        public BidDto AddItem(BidDto bid)
        {
            return _bidRepository.Create(bid.ToDal()).ToDto();
        }

        public void Update(BidDto bid)
        {
            var orm = _bidRepository.GetById(bid.BidId);
            orm = bid.ToDal(orm);
            _bidRepository.Update(orm);
        }

        public void RemoveItem(int id)
        {
            Bid bid = _bidRepository.GetById(id);
            _bidRepository.Delete(bid);
        }

        public IEnumerable<BidDto> Find(Expression<Func<BidDto, bool>> predicate)
        {
            throw new NotImplementedException();
            //return _bidRepository.Find(predicate);
        }

        public IEnumerable<BidDto> GetAllBidsForLotWithUsers(int lodId)
        {
            return _bidRepository.GetAllBidsForLotWithUsers(lodId).ToDto();
        }

        public IEnumerable<BidDto> GetAllBidsForUser(int userId)
        {
            return _bidRepository.GetAllBidsForUser(userId).ToDto();
        }

        public BidDto GetLastBidForLot(int lotId)
        {
            return _bidRepository.GetLastBidForLot(lotId).ToDto();
        }

    }
}