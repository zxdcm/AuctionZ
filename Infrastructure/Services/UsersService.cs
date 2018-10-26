using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Services
{
    public class UsersService : IUserServices
    {

        private readonly IUserRepository _userRepository;
    
        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetItem(int id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetItems()
        {
            return _userRepository.ListAll();
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _userRepository.Find(predicate);
        }

        public User AddItem(User user)
        {
            return _userRepository.Create(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }


        public void DepositMoneyToUser(decimal amount, int userId)
        {
            var user = this.GetItem(userId);
            user.Money += amount;
            this.Update(user);
        }

        public void WithDrawMoneyFromUser(decimal amount, int userId)
        {
            var user = this.GetItem(userId);
            user.Money -= amount;
            this.Update(user);
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}