using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryByName(string name);
        Category GetCategoryByNameWithLots(string name);
        int AmountOfLots(int id);
        IEnumerable<Category> GetAllCategoriesWithLots();
    }
}