using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionZ.Models
{
    public class LotViewModel
    {
        [ScaffoldColumn(false)]
        public int LotId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Starting price")]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "10000", ErrorMessage = "Invalid price")]
        [Required(ErrorMessage = "The field must be filled")]
        public decimal Price { get; set; }
        public byte[] Image { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Expiration time")]
        [Required(ErrorMessage = "The field must be filled")]
        public DateTime ExpirationTime { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "The category is required!")]
        public string CategoryId { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<Bid> Bids { get; set; }


    }
}