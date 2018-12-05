using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace AuctionZ.Controllers
{
    public class BaseController : Controller
    {
        protected int UserId => 
            int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier).Value);

        protected string UserName => User.Identity.Name;
    }
}