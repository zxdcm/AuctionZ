using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AuctionZ.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionZ.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {

            ViewData["Message"] = "Your application description page.";
            return View();
        }

        private void delegte(object sender, ConsoleCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            int z = 10;
            if (z == Null)
            {

            }
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
