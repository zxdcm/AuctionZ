using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionZ.Models
{
    public class ProfileViewModel
    {
        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        public bool EmailConfirmed { get; set; }
        
        public decimal Money { get; set; }

    }
}
