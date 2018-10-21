using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Services
{
    public class BidsService : IBidsService
    {
        private readonly IBidRepository _bidRepository;

        public BidsService(IBidRepository bidRepository)
        {
            this._bidRepository = bidRepository;
        }


        public Bid GetItem(int id)
        {
            return _bidRepository.GetById(id);
        }

        public IEnumerable<Bid> GetItems()
        {
            return _bidRepository.ListAll();
        }

        public Bid AddItem(Bid bid)
        {
            return _bidRepository.Create(bid);
        }

        public void Update(Bid bid)
        {
            _bidRepository.Update(bid);
        }

        public void RemoveItem(int id)
        {
            Bid bid = _bidRepository.GetById(id);
            _bidRepository.Delete(bid);
        }

        public IEnumerable<Bid> Find(Func<Bid, bool> predicate)
        {
            return _bidRepository.Find(predicate);
        }

        public IEnumerable<Bid> GetAllBidsForLotWithUsers(int lodId)
        {
            return _bidRepository.GetAllBidsForLotWithUsers(lodId);
        }

        public IEnumerable<Bid> GetAllBidsForUser(int userId)
        {
            return _bidRepository.GetAllBidsForUser(userId);
        }

        public Bid GetLastBidForLot(int lotId)
        {
            return _bidRepository.GetLastBidForLot(lotId);
        }

    }
}