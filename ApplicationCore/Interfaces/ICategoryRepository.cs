using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryByName(string name);
    }
}