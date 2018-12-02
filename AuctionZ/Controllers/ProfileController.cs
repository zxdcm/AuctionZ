using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AuctionZ.Models;
using AuctionZ.Models.MappingProfiles;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace AuctionZ.Controllers
{
    public class ProfileController : Controller
    {

        private readonly ILotsService _lotsService;
        private readonly IBidsService _bidsService;
        private readonly IUserServices _usersServices;
        private readonly int USER_ID;
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IHostingEnvironment environment, ILogger<ProfileController> logger, 
            IMapper mapper, ILotsService lotsService, IBidsService bidsService,
            IUserServices usersService, ICategoryRepository categoryRepository)
        {
            _environment = environment;
            _lotsService = lotsService;
            _bidsService = bidsService;
            _usersServices = usersService;
            USER_ID = 2; // Todo remove
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditLot(int lotId)
        {
            var lotVm = _lotsService.GetItem(lotId).ToVm();
            lotVm.Categories = GetCategories();
            return View(lotVm);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditLot(int lotId, LotViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _logger.LogCritical($"{vm.ImageFile?.Name} ImageFile NULL ? {vm.ImageFile == null}");
                SaveImage(vm);
                var lot = vm.ToDto();
                _lotsService.Update(lot);
                return RedirectToAction(nameof(AuctionController.Details), "Auction", new { lotId = lot.LotId}); //Replace hardcored call
                //return RedirectToAction(nameof(Details), new { lotId = lot.LotId });
            }
            // Refetch data
            vm.Categories = GetCategories();
            return View(vm);
        }


        [HttpGet]
        public IActionResult CreateLot()
        {
            var vm = new LotViewModel() { UserId = USER_ID }; //TOdo replace
            vm.Categories = GetCategories();
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateLot(LotViewModel vm)
        {
            if (ModelState.IsValid)
            {
                SaveImage(vm);
                var lot = vm.ToDto();
                lot = _lotsService.AddItem(lot);
                return RedirectToAction(nameof(MyLots), lot.LotId);
                //return RedirectToAction(nameof(Details), new { lotId = lot.LotId });
            }

            vm.Categories = GetCategories();
            return View(vm);
        }

        [HttpGet]
        public IActionResult DeleteLot(int lotId)
        {
            var lotVm = _lotsService.GetItem(lotId).ToVm();
            return View(lotVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLotConfirmed(int lotId)
        {
            _lotsService.RemoveItem(lotId);
            return RedirectToAction(nameof(MyLots));
        }

        public IActionResult MyLots()
        {
            var lots = _lotsService.GetAllLotsForUser(USER_ID).ToVm();
            return View(lots);
        }

        public IActionResult MyBids()
        {
            var lots = _bidsService.GetAllBidsForUser(USER_ID).ToVm();
            return View(lots);
        }

        public IActionResult MyPurchases()
        {
            return View();
            //var lots = _mapper.Map<IEnumerable<LotViewModel>>(_bidsService.GetAllLotsForUser(USER_ID));
            //return View(lots);
        }


        [NonAction]
        public IEnumerable<SelectListItem> GetCategories()
        {
            var categoryRepository = HttpContext.RequestServices.GetRequiredService<ICategoryRepository>();
            return categoryRepository.ListAll()
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                });
        }

        [NonAction]
        public void SaveImage(LotViewModel vm)
        {
            if (vm.ImageFile != null)
            {
                var fileName = GetUniqueName(vm.ImageFile.FileName);
                var uploads = Path.Combine(_environment.WebRootPath, "images");
                var filePath = Path.Combine(uploads, fileName);
                vm.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                vm.ImageUrl = fileName;
            }
            else if (vm.ImageUrl == null)
                vm.ImageUrl = "no_image.jpg";
        }

        [NonAction]
        public string GetUniqueName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}