using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<LotHub> _hubcontext;

        public AuctionController(ILogger<AuctionController> logger,
            ILotsService lotsService, IBidsService bidsService,
            IUserServices userServices, IHubContext<LotHub> hubcontext)
        {
            _lotsService = lotsService;
            _bidsService = bidsService;
            _usersServices = userServices;
            _hubcontext = hubcontext;
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
        [Authorize(Roles="admin,user")]
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
        [Authorize(Roles = "admin,user")]
        public IActionResult EditLot(int lotId, LotViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.ImageUrl = SaveImage(vm.ImageFile,
                    HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>());
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
        [Authorize]
        public async Task<IActionResult> MakeBid([FromBody]BidDto bid)
        {
            var lot = _lotsService.GetItem(bid.LotId);
            if (lot == null)
                return BadRequest();
            var user = _usersServices.GetItem(UserId);
            if (lot.Price >= bid.Price)
                ModelState.AddModelError(nameof(bid.Price), "Bid must be greater than value price");
            if (user.Money < bid.Price)
                ModelState.AddModelError(nameof(bid.Price), "Not enough funds");
            if (ModelState.IsValid)
            {
                _usersServices.MakeBid(lot.LotId, UserId, bid.Price);
                var lastbid = _bidsService.GetLastBidForLot(bid.LotId);
                var row_info = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr", lastbid.User.UserName,
                        lastbid.Price.ToString("C"), lastbid.DateOfBid.ToString("f"));
                await _hubcontext.Clients.All.SendAsync("BidMade",
                  lot.LotId, row_info, lastbid.Price.ToString("C"));
                return Ok();
                //return new ObjectResult(new {row = row_info});
            }
            return new BadRequestObjectResult(ModelState);
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