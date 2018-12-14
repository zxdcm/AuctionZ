using System;
using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.DTO
{
    public class LotDto
    {
        public int LotId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ExpirationTime { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public List<BidDto> Bids { get; set; }
        public bool IsFinished { get; set; }

    }
}