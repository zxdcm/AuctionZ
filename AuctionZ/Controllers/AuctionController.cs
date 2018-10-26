using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AuctionZ.Models;
using AuctionZ.Models.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionZ.Controllers
{
    public class AuctionController : Controller
    {
        private readonly ILotsService _lotService;
        private readonly IBidsService _bidService;
        private readonly IUserServices _userServices;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public AuctionController(IMapper mapper, ILotsService lotService, IBidsService bidService, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _lotService = lotService;
            _bidService = bidService;
            _categoryRepository = categoryRepository;
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

        public IActionResult Details(int lotId)
        {
            var lot_vm = _mapper.Map<LotViewModel>(_lotService.GetItem(lotId));
            lot_vm.Bids = _bidService.GetAllBidsForLotWithUsers(lot_vm.LotId);
            return View(lot_vm);
        }

        [HttpPost]
        public IActionResult MakeBid(int lotId, decimal bidValue)
        {
            
            var lot = _lotService.GetItem(lotId);
            if (lot.Price < bidValue)
            {
                ModelState.AddModelError(nameof(bidValue), "Wrong range of value");
                return RedirectToAction(nameof(Details), new { lotId = lot.LotId});
            }

            if (ModelState.IsValid)
            {
                using (TransactionScope transaction = new TransactionScope())
                {

                    var last_bid = _bidService.GetLastBidForLot(lotId);

                    if (last_bid != null)
                        _userServices.DepositMoneyToUser(last_bid.Price, last_bid.UserId);

                    var bidder_id = 2; //todo fix
                    _userServices.WithDrawMoneyFromUser(bidValue, bidder_id);

                    _bidService.AddItem(new Bid()
                    {
                        DateOfBid = DateTime.Now,
                        LotId = lot.LotId,
                        Price = bidValue,
                        UserId = 2
                    });

                    lot.Price = bidValue;
                    _lotService.Update(lot);

                    transaction.Complete();
                }
            }
            return RedirectToAction(nameof(Details), lot.LotId);
        }

        [HttpGet]
        public IActionResult EditLot(int lotId)
        {
            var lot = _lotService.GetItem(lotId);
            var lotvm = _mapper.Map<LotViewModel>(lot);
            lotvm.Categories = GetCategories();
            return View(lotvm);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditLot(int lotId, LotViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var lot = _mapper.Map<Lot>(vm);
                _lotService.Update(lot);
                return RedirectToAction(nameof(Details), new { lotId = lot.LotId });
            }
            // Refetch data
            vm.Categories = GetCategories();
            return View(vm);
        }


        [HttpGet]
        public IActionResult CreateLot()
        {
            var vm = new LotViewModel(){ UserId = 1}; //TOdo replace
            vm.Categories = GetCategories();
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateLot(LotViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var lot = _mapper.Map<Lot>(vm);
                lot = _lotService.AddItem(lot);
                return RedirectToAction(nameof(Details), new {lotId = lot.LotId});
            }

            vm.Categories = GetCategories();
            return View(vm);
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetCategories()
        {
            return _categoryRepository.ListAll()
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                });
        }

    }
}