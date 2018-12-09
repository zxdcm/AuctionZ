using System.Threading.Tasks;
using ApplicationCore.DTO;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Interfaces
{
    public interface IUserServices : IManagementService<UserDto>
    {
        void MakeBid(int lotId, int bidderId, decimal bidValue);
        Task<IdentityResult> TryRegister(UserDto user, string password);
        Task<SignInResult> TryLogin(string email, string password);
        Task Logout();
        Task<bool> IsInRoleAsync(UserDto user, string role);
        Task<IdentityResult> AddToRoleAsync(UserDto user, string role);
        Task<IdentityResult> RemoveFromRoleAsync(UserDto user, string role);

    }
}