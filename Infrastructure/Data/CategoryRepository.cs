using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public Category GetCategoryByNameWithLots(string name)
        {
            return _context.Categories.Where(x => x.Name == name)
                .Include(x => x.Lots).First();
        }

        public int AmountOfLots(int id)
        {
            return _context.Lots.Count(x => x.CategoryId == id);
        }

        public IEnumerable<Category> GetAllCategoriesWithLots()
        {
            return _context.Categories.Include(x => x.Lots);
        }
    }
}