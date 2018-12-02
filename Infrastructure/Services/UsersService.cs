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

        private readonly IUserRepository _userRepository;
        //        private readonly Lazy<UserManager<User>> _userManager;
        //        private readonly Lazy<RoleManager<Role>> _roleManager;
        //        private readonly Lazy<SignInManager<User>> _signinManager;

        //        private UserManager<User> UserManager => _userManager.Value;
        //        private SignInManager<User> SigninManager => _signinManager.Value;

        //        private UserManager<User> UserManager => _userManager.Value;
        //        private SignInManager<User> SigninManager => _signinManager.Value;

        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SigninManager;


        public UsersService(IUserRepository userRepository, 
            UserManager<User> userManager,
            SignInManager<User> signinManager)
//            Lazy<UserManager<User>> userManager, 
//            Lazy<RoleManager<Role>> roleManager, 
//            Lazy<SignInManager<User>> signinManager)
        {
            UserManager = userManager;
            SigninManager = signinManager;
//            _userManager = userManager;
//            _signinManager = signinManager;
        }

        public UserDto GetItem(int id)
        {
            return _userRepository.GetById(id).ToDto();
        }

        public IEnumerable<UserDto> GetItems()
        {
            return _userRepository.ListAll().ToDto();
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


        public async Task<IdentityResult> TryRegister(UserDto user, string password) =>
            await UserManager.CreateAsync(user.ToDal(), password);


        public async Task<SignInResult> TryLogin(string email, string password)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                await SigninManager.SignOutAsync();
                return await SigninManager.PasswordSignInAsync(user, password, false, false);
            }
            return null;
        }

        public async Task Logout() => await SigninManager.SignOutAsync();

    }
}