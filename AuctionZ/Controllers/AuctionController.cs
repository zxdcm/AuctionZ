using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ApplicationCore;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AuctionZ.Models;
using AuctionZ.Models.MappingProfiles;
using AuctionZ.Models.Utils;
using AutoMapper;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static AuctionZ.Utils.Utils;

namespace AuctionZ.Controllers
{
    public class AuctionController : BaseController
    {
        private readonly ILotsService _lotsService;
        private readonly IBidsService _bidsService;
        private readonly IUserServices _usersServices;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(ILogger<AuctionController> logger,
            ILotsService lotsService, IBidsService bidsService,
            IUserServices userServices)
        {
            _lotsService = lotsService;
            _bidsService = bidsService;
            _usersServices = userServices;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(int? category, string title, int page=1)
        {
           int pageSize = 4;
           var criteria = new LotsFilterCriteria()
                {Page = page, Category = category, Title = title, PageSize = pageSize, Active = true};
           var lots = _lotsService.GetAllLots(criteria).ToVm();
           var model = new LotsViewModel()
           {
               Lots = lots,
               Pagination = new PageViewModel(_lotsService.GetLotsCount(criteria), page, pageSize),
               Filter = new FilterViewModel(GetCategories(), title, category)
           };       
           return View(model);
        }

        public IActionResult Details(int lotId)
        {
            var lot_vm = _lotsService.GetItem(lotId).ToVm();
            lot_vm.Bids = _bidsService.GetAllBidsForLotWithUsers(lot_vm.LotId)
                .OrderByDescending(x => x.DateOfBid).ToVm();
            return View(lot_vm);
        }

        [HttpGet]
        [Authorize(Roles="Admin,User")]
        public IActionResult EditLot(int lotId)
        {
            var lot = _lotsService.GetItem(lotId);
            if (lot == null || (lot.UserId != UserId))
                return StatusCode(404);
            var lotVm = lot.ToVm();
            lotVm.Categories= GetCategoriesSelectListItems(lot.CategoryId);
            return View(lotVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public IActionResult EditLot(int lotId, LotViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.ImageFile != null)
                    vm.ImageUrl = SaveImage(vm.ImageFile, 
                        HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>()) ?? "no_image.jpg";
                var lot = vm.ToDto();
                _lotsService.Update(lot);
                return RedirectToAction(nameof(Details), new { lotId = lot.LotId}); 
            }
            // Re-fetch data
            vm.Categories = GetCategoriesSelectListItems(vm.CategoryId);
            return View(vm);
        }

        [HttpGet]
        public IActionResult DeleteLot(int lotId)
        {
            var lotVm = _lotsService.GetItem(lotId).ToVm();
            return View(lotVm);
        }

        [HttpPost]
        [ActionName("DeleteLot")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLotConfirmed(int lotId)
        {
            _lotsService.RemoveItem(lotId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
//
//        [HttpPost("{id}")]
//        [Authorize]
//        public IActioResult MakeBid(int lotId, [FromBody]decimal bidValue)
//        {
//            return 
//        }

        private IEnumerable<CategoryDto> GetCategories()
        {
            var categoryService = HttpContext.RequestServices.GetRequiredService<ICategoryService>();
            return categoryService.GetItems();
        }

        private IEnumerable<SelectListItem> GetCategoriesSelectListItems(int? selectedId=0)
        {
            return GetCategories()
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString(),
                    Selected = selectedId == c.CategoryId
                });
        }

    }
}