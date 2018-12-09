using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AuctionZ.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private IUserServices _userServices;
        private IRoleService _roleService;

        public RoleUsersTagHelper(IUserServices userServices,
            IRoleService roleServices)
        {
            _userServices = userServices;
            _roleService = roleServices;
        }

        [HtmlAttributeName("identity-role")] public int Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context,
            TagHelperOutput output)
        {

            List<string> names = new List<string>();
            var role = await _roleService.GetByIdAsync(Role);
            if (role != null)
            {
                foreach (var user in _userServices.GetItems())
                {
                    if (user != null
                        && await _userServices.IsInRoleAsync(user, role.Name))
                        names.Add(user.UserName);
                }
            }

            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
    }
}
