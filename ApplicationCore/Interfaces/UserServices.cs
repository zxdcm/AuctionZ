using ApplicationCore.DTO;

namespace ApplicationCore.Interfaces
{
    public interface IUserServices : IManagementService<UserDto>
    {
        void MakeBid(int lotId, int bidderId, decimal bidValue);
    }
}