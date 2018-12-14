using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class UsersService : IUserServices
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository, 
            UserManager<User> userManager,
            SignInManager<User> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _userRepository = userRepository;
        }

        public UserDto GetItem(int id)
        {
            return _userRepository.GetById(id).ToDto();
        }

        public IEnumerable<UserDto> GetItems()
        {
            return _userRepository.ListAll()?.ToDto();
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
            var orm = _userRepository.GetById(user.UserId);
            orm = user.ToDal(orm);
            _userRepository.Update(orm);
        }

        public void MakeBid(int lotId, int bidderId, decimal bidValue)
        {
            _userRepository.MakeBid(lotId, bidderId, bidValue);
        }


        public async Task<IdentityResult> TryRegister(UserDto user, string password)  {
            var res = await _userManager.CreateAsync(user.ToDal(), password);
            if (!res.Succeeded)
                return res;
            var orm = await _userManager.FindByEmailAsync(user.Email);
            return await _userManager.AddToRoleAsync(orm, "user");
        }


        public async Task<SignInResult> TryLogin(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _signinManager.SignOutAsync();
                return await _signinManager.PasswordSignInAsync(user, password, false, false);
            }
            return null;
        }

        public async Task<bool> IsInRoleAsync(UserDto user, string role)
        {
            return await _userManager.IsInRoleAsync(user.ToDal(), role);
        }

        public async Task<IdentityResult> AddToRoleAsync(UserDto user, string role)
        {
            var orm = await _userManager.FindByIdAsync(user.UserId.ToString());
            return await _userManager.AddToRoleAsync(orm, role);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(UserDto user, string role)
        {
            var orm = await _userManager.FindByIdAsync(user.UserId.ToString());
            return await _userManager.RemoveFromRoleAsync(orm, role);
        }

        public async Task Logout() => await _signinManager.SignOutAsync();

    }
}