using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void MakeBid(int lotId, int userId, decimal bidValue);
    }
}