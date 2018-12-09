using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AuctionZ.Models;
using AuctionZ.Models.MappingProfiles;
using AuctionZ.Models.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static AuctionZ.Utils.Utils;
using AuctionZ.Models.MappingProfiles;

namespace AuctionZ.Controllers
{

    [Authorize]
    public class ProfileController : BaseController
    {

        private readonly ILotsService _lotsService;
        private readonly IBidsService _bidsService;
        private readonly IUserServices _usersServices;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger,
            ILotsService lotsService, 
            IBidsService bidsService,
            IUserServices usersService, ICategoryRepository categoryRepository)
        {
            _lotsService = lotsService;
            _bidsService = bidsService;
            _usersServices = usersService;
        }

        public IActionResult Index() => RedirectToAction(nameof(Info));

        [HttpGet]
        public IActionResult Info()
        {
            var profile = _usersServices.GetItem(UserId).ToVm();
            return View(profile);
        }

        [HttpPost]
        public IActionResult Info(ProfileViewModel vm)
        {
            _usersServices.Update(vm.ToDto());
            return RedirectToAction(nameof(Info));
        }

        public IActionResult Lots(int? category, string title, int page = 1, bool? active=null)
        {
            int pageSize = 4;
            var criteria = new LotsFilterCriteria()
                { Page = page, Category = category, Title = title, PageSize = pageSize, Active = active, UserId = UserId};
            var lots = _lotsService.GetAllLots(criteria).ToVm();
            var model = new LotsViewModel()
            {
                Lots = lots,
                Pagination = new PageViewModel(_lotsService.GetLotsCount(criteria), page, pageSize),
                Filter = new FilterViewModel(GetCategories(), title, category, active)
            };
            return View(model);
        }

        public IActionResult Bids()
        {
            var lots = _bidsService.GetAllBidsForUser(UserId).ToVm();
            return View(lots);
        }


        public IActionResult Purchases()
        {
            var lots = _lotsService.GetUserPurchases(UserId).ToVm();
            return View(lots);
        }

        [HttpGet]
        public IActionResult CreateLot()
        {
            var vm = new LotViewModel {UserId = UserId, Categories = GetCategoriesSelectListItems()};
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateLot(LotViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.ImageFile != null)
                    vm.ImageUrl = SaveImage(vm.ImageFile,
                                      HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>()) ?? "no_image.jpg";;
                var lot = _lotsService.AddItem(vm.ToDto());
                return RedirectToAction(nameof(Lots), lot.LotId);
            }

            vm.Categories = GetCategoriesSelectListItems(vm.CategoryId);
            return View(vm);
        }

        private IEnumerable<CategoryDto> GetCategories()
        {
            var categoryService = HttpContext.RequestServices.GetRequiredService<ICategoryService>();
            return categoryService.GetItems();
        }

        private IEnumerable<SelectListItem> GetCategoriesSelectListItems(int? selectedId = 0)
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