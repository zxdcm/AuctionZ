using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Mappers;

namespace Infrastructure.Services
{
    public class UsersService : IUserServices
    {

        private readonly IUserRepository _userRepository;
    
        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto GetItem(int id)
        {
            return _userRepository.GetById(id).ToDto();
        }

        public IEnumerable<UserDto> GetItems()
        {
            return _userRepository.ListAll().Select(x => x.ToDto());
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> Find(Expression<Func<UserDto, bool>> predicate)
        {
            throw new NotImplementedException();
//            return _userRepository.Find(predicate);
        }

        public UserDto AddItem(UserDto user)
        {
            return _userRepository.Create(user.ToDal()).ToDto();
        }

        public void Update(UserDto user)
        {
            _userRepository.Update(user.ToDal());
        }

        public void MakeBid(int lotId, int bidderId, decimal bidValue)
        {
            _userRepository.MakeBid(lotId, bidderId, bidValue);
        }


    }
}