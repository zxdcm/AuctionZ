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

    }
}