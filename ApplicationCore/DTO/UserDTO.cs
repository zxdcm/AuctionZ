using System.Collections.Generic;

namespace ApplicationCore.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public decimal Money { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<BidDto> Bids { get; set; }
        public List<LotDto> Lots { get; set; }
    }
}