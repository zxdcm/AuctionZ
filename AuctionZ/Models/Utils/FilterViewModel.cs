using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionZ.Models.Utils
{
    public class FilterViewModel
    {
        public FilterViewModel(IEnumerable<CategoryDto> categories, string title,
            int? selectedCategory, bool? active=true, string userName=null)
        {
            var categoriesList = categories.ToList(); //Fix somehow later
            categoriesList.Insert(0, new CategoryDto() {Name = "All", CategoryId = 0});
            Categories = new SelectList(categoriesList, "CategoryId", "Name", selectedCategory);
            SelectedTitle = title;
            SelectedCategory = selectedCategory;
            Active = active;
            UserName = userName;
        }
        public SelectList Categories { get; } 
        public int? SelectedCategory { get; }   
        public string SelectedTitle { get; }
        public bool? Active { get; } 
        public string UserName { get; }
    }
}
