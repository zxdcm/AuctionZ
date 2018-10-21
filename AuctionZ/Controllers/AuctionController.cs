using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AuctionZ.Models;
using AuctionZ.Models.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionZ.Controllers
{
    public class AuctionController : Controller
    {
        private readonly ILotsService _lotService;
        private readonly IBidsService _bidService;
        private readonly int pageSize = 3;
        private readonly IMapper _mapper;

        public AuctionController(IMapper mapper, ILotsService lotService, IBidsService bidService)
        {
            _mapper = mapper;
            _lotService = lotService;
            _bidService = bidService;
        }

        public IActionResult Index()
        {
          var lots = _mapper.Map<IEnumerable<LotViewModel>>(_lotService.GetItems()
              .OrderByDescending(x => x.ExpirationTime));
            if (lots.Count() == 0)
                ViewBag.Result = "No result";
           var model = new ResultListViewModel<LotViewModel>(lots, 3, 3);       
           return View(model);
        }

        public IActionResult Details(int id)
        {
            var lot_vm = _mapper.Map<LotViewModel>(_lotService.GetItem(id));
            lot_vm.Bids = _bidService.GetAllBidsForLotWithUsers(lot_vm.LotId);
            return View(lot_vm);
        }

        public IActionResult MakeBid(int id)
        {
            return View();
        }

    }
}