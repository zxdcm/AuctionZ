using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities
{
    public class Role : IdentityRole<int>
    {

        public int RoleId
        {
            get => Id;
            set => Id = value;
        }

        public Role(string roleName) : base(roleName) { }
        public Role() {  }
    }
}