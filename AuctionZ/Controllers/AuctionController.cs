using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AuctionZ.Models;
using AuctionZ.Models.MappingProfiles;
using AuctionZ.Models.Utils;
using AutoMapper;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AuctionZ.Controllers
{
    public class AuctionController : Controller
    {
        private readonly ILotsService _lotsService;
        private readonly IBidsService _bidsService;
        private readonly IUserServices _usersServices;
        private readonly ICategoryRepository _categoryRepository;
        private readonly int UserId = 1;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(IMapper mapper, ILogger<AuctionController> logger,
            ILotsService lotsService, IBidsService bidsService,
            IUserServices userServices, ICategoryRepository categoryRepository)
        {
            _lotsService = lotsService;
            _bidsService = bidsService;
            _usersServices = userServices;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
          var lots = _lotsService.GetItems()
              .OrderByDescending(x => x.ExpirationTime).ToVm();
            if (lots.Count() == 0)
                ViewBag.Result = "No result";
           var model = new ResultListViewModel<LotViewModel>(lots, 3, 3);       
           return View(model);
        }

        public IActionResult Details(int lotId)
        {
            var lot_vm = _lotsService.GetItem(lotId).ToVm();
            lot_vm.Bids = _bidsService.GetAllBidsForLotWithUsers(lot_vm.LotId)
                .OrderByDescending(x => x.DateOfBid).ToVm();
            return View(lot_vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeBid(int lotId, decimal bidValue)
        {
            var lot = _lotsService.GetItem(lotId);
            var user = _usersServices.GetItem(UserId);
            if (lot.Price > bidValue)
                ModelState.AddModelError(nameof(bidValue), "Wrong range of value");

            if (user.Money < bidValue)
                ModelState.AddModelError(nameof(bidValue), "Not enough funds");

            if (ModelState.IsValid)
                _usersServices.MakeBid(lot.LotId, UserId, bidValue);

            return RedirectToAction(nameof(Details), new { lotId = lot.LotId });
        }


    }
}