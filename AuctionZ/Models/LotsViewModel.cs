using System.Collections.Generic;
using System.Linq;
using AuctionZ.Models.Utils;

namespace AuctionZ.Models
{
    public class LotsViewModel
    {
        public IEnumerable<LotViewModel> Lots { get; set; }
        public PageViewModel Pagination { get; set; }
        public FilterViewModel Filter { get; set; }
    }
}