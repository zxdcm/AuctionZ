using System.Collections;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public int IdentityId { get; set; }
        public decimal Money { get; set; }
        public string Name { get; set; }

        public ICollection<Bid> Bids { get; set; }
        public ICollection<Lot> Lots { get; set; }


    }
}