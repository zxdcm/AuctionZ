using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace AuctionZ.Controllers
{
    public class BaseController : Controller
    {
        protected int UserId => 
            int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier).Value);

        protected string UserName => User.Identity.Name;
        protected bool IsAdmin => User.IsInRole("admin");

        protected int? GetUserIdByUserName(string userName)
        {
            if (userName == null)
                return null;
            var manager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var id =  manager.FindByNameAsync(userName)?.Id;
            return id ?? 0;
        }
    }
}