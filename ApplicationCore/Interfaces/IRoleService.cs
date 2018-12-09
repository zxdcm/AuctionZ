using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Interfaces
{
    public interface IRoleService : IManagementService<RoleDto>
    {
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(RoleDto role);
        Task<RoleDto> GetByIdAsync(int id);
        IEnumerable<RoleDto> GetRoles();
    }
}