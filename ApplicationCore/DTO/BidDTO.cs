using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;

namespace ApplicationCore.DTO
{
    public class BidDto
    {
        public int BidId { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfBid { get; set; }

        public int LotId { get; set; }
        public LotDto Lot { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }

    }
}
