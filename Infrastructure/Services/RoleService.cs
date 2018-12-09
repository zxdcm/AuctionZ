using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await _roleManager.CreateAsync(new Role(roleName));
        }

        public async Task<IdentityResult> DeleteRoleAsync(RoleDto role)
        {
            var orm = await _roleManager.FindByIdAsync(role.Id.ToString());
            return await _roleManager.DeleteAsync(orm);
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return role.ToDto();
        }

        public IEnumerable<RoleDto> GetRoles() => _roleManager.Roles.AsEnumerable().ToDto();


        public RoleDto AddItem(RoleDto item)
        {
            throw new NotImplementedException();
        }

        public RoleDto GetItem(int id)
        {
            return _roleRepository.GetById(id).ToDto();
        }

        public IEnumerable<RoleDto> GetItems()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(RoleDto item)
        {
            throw new NotImplementedException();
        }
    }
}
