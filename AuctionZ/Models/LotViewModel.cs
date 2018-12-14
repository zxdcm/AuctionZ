using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace AuctionZ.Models
{
    public class LotViewModel : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public int LotId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Starting price")]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "1", "10000", ErrorMessage = "Invalid price")]
        [Required(ErrorMessage = "The field must be filled")]
        public decimal Price { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Expiration time")]
        [Required(ErrorMessage = "The field must be filled")]
        public DateTime ExpirationTime { get; set; } = DateTime.Now.AddDays(7);

        [Display(Name = "Category")]
        [Required(ErrorMessage = "The category is required!")]
        public int CategoryId { get; set; }
        
        public string Description { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }

        [Display(Name ="Image")]
        public string ImageUrl { get; set; }

        //Optional
        public IEnumerable<BidViewModel> Bids { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (ExpirationTime < DateTime.Now)
            {
                results.Add(new ValidationResult($"Expiration date must be greater than {DateTime.Now}", new[] { "ExpirationTime" }));
            }
            return results;
        }
    }
}