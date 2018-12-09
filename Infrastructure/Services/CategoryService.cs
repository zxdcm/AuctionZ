using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Mappers;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryDto AddItem(CategoryDto item)
        {
            return _categoryRepository.Create(item.ToDal()).ToDto();
        }

        public CategoryDto GetItem(int id)
        {
            return _categoryRepository.GetById(id).ToDto();
        }

        public IEnumerable<CategoryDto> GetItems()
        {
            return _categoryRepository.ListAll().ToDto();
        }

        public void RemoveItem(int id)
        {
            var category = _categoryRepository.GetById(id);
            _categoryRepository.Delete(category);
        }

        public void Update(CategoryDto item)
        {
            var orm = _categoryRepository.GetById(item.CategoryId);
            orm = item.ToDal(orm); // Copy fields of dto to orm entity
            _categoryRepository.Update(orm); // Commit changes
        }

        public int AmountOfLots(int id)
        {
            return _categoryRepository.AmountOfLots(id);
        }

        public CategoryDto GetCategoryByName(string name)
        {
            return _categoryRepository.GetCategoryByName(name).ToDto();
        }

        public CategoryDto GetCategoryByNameWithLots(string name)
        {
            return _categoryRepository.GetCategoryByNameWithLots(name).ToDto();
        }

        public IEnumerable<CategoryDto> GetAllCategoriesWithLots()
        {
            return _categoryRepository.GetAllCategoriesWithLots().ToDto();
        }
    }
}
