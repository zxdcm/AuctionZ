using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data
{
    public class RoleRepository : EfRepository<Role>, IRoleRepository
    {
        public RoleRepository(AuctionContext dbContext) : base(dbContext)
        {

        }
    }
}