using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Services
{
    public class LotsService : ILotsService
    {

        private readonly ILotRepository _lotRepository; 

        public LotsService(ILotRepository lotRepository)
        {
            this._lotRepository = lotRepository;
        }

        public Lot GetItem(int id)
        {
            return _lotRepository.GetById(id);
        }

        public IEnumerable<Lot> GetItems()
        {
            return _lotRepository.ListAll();
        }

        public IEnumerable<Lot> Find(Func<Lot, bool> predicate)
        {
            return _lotRepository.Find(predicate);
        }

        public Lot AddItem(Lot lot)
        {
            return _lotRepository.Create(lot);
        }

        public void Update(Lot lot)
        {
            _lotRepository.Update(lot);
        }

        public void RemoveItem(int id)
        {
            Lot lot = _lotRepository.GetById(id);
            _lotRepository.Delete(lot);
        }

        public Lot GetByIdWithBids(int id)
        {
            return _lotRepository.GetByIdWithBids(id);
        }

        public IEnumerable<Lot> GetAllWithBids()
        {
            return _lotRepository.GetAllWithBids();
        }

        public IEnumerable<Lot> FindWidthBids(Func<Lot, bool> predicate)
        {
            return _lotRepository.FindWithBids(predicate);
        }

        }
      
   
}