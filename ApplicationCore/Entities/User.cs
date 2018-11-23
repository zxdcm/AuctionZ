using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ApplicationCore.Entities
{
    public class User : IdentityUser
    {
        public decimal Money { get; set; }
        public string Name { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}