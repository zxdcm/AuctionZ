using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionZ.Models
{
    public class BidViewModel
    {
        [ScaffoldColumn(false)]
        public int BidId { get; set; }
        public DateTime DateOfBid { get; set; }
        public string Price { get; set; }
        public int LotId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
