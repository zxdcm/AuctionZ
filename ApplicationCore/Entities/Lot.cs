using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Lot
    {
        public int LotId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}