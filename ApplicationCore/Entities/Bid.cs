using System;

namespace ApplicationCore.Entities
{
    public class Bid
    {
        public int BidId { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfBid { get; set; }

        public int LotId { get; set; } 
        public virtual Lot Lot { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } 
    }
}