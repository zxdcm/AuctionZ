using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {

        public UserRepository(AuctionContext dbContext) : base(dbContext)
        {

        }

        public void DepositMoneyToUser(decimal amount, int userId)
        {
            
        }

        public void WithDrawMoneyFromUser(decimal amount, int userId)
        {
            throw new System.NotImplementedException();
        }


    }
}