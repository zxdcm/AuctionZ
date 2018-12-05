
using System.Collections.Generic;
using ApplicationCore.DTO;

namespace ApplicationCore.Interfaces
{
    public interface ICategoryService : IManagementService<CategoryDto>
    {
        CategoryDto GetCategoryByName(string name);
        CategoryDto GetCategoryByNameWithLots(string name);
        int AmountOfLots(int id);
        IEnumerable<CategoryDto> GetAllCategoriesWithLots();
    }
}