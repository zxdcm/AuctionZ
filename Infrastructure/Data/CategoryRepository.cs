using System.Linq;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data
{
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(AuctionContext context) : base(context)
        {

        }

        public Category GetCategoryByName(string name)
        {
            return _context.Categories
                .FirstOrDefault(c => c.Name == name);
        }
    }
}