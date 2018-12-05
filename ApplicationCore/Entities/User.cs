using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities
{
    public class User : IdentityUser<int>
    {
        [NotMapped]
        public int UserId
        {
            get => Id;
            set => Id = value;
        }

        public decimal Money { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Lot> Lots { get; set; }


    }
}