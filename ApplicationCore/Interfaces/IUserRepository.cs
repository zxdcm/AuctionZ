using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void DepositMoneyToUser(decimal amount, int userId);
        void WithDrawMoneyFromUser(decimal amount, int userId);
    }
}