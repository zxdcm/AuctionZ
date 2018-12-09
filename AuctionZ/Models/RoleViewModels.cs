using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Entities;

namespace AuctionZ.Models
{
    public class RoleViewModel
    {
        public RoleDto Role { get; set; }
        public IEnumerable<UserDto> Members { get; set; }
        public IEnumerable<UserDto> NonMembers { get; set; }
    }

    public class RoleModificationViewModel
    {
        [Required]
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int[] IdsToAdd { get; set; }
        public int[] IdsToDelete { get; set; }
    }

}
