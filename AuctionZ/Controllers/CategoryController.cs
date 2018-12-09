using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuctionZ.Controllers
{
    [Route("api/categories")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService,
            ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View("~/Views/Admin/Categories");
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] string name)
        {
            _logger.LogCritical(name);
            var duplicate = _categoryService.GetCategoryByName(name);
            if (duplicate != null)
            {
                return BadRequest("Category with this name already exist");
            }
            var category = _categoryService.AddItem(new CategoryDto() {Name = name});
            return new ObjectResult(category);
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetItems();
            return new ObjectResult(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetItem(id);
            if (category == null)
                return BadRequest();
            return new ObjectResult(category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetItem(id); 
            if (category == null)
                return NotFound();
            var lotsCount = _categoryService.AmountOfLots(id);
            if (lotsCount > 0)
                return BadRequest($"Category cant be deleted. It has {lotsCount} attached to it");
            _categoryService.RemoveItem(id);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public IActionResult EditCategory([FromBody]CategoryDto category)
        {
            if (_categoryService.GetItem(category.CategoryId) == null)
                return NotFound();
            _categoryService.Update(category);
            return Ok(category);
        }

    }
}