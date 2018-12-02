using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using AuctionZ.Models.Account;
using AuctionZ.Models.MappingProfiles;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AuctionZ.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserServices _userService;

        public AccountController(IUserServices userServices)
        {
            _userService = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.TryRegister(vm.ToDto(), vm.Password);
                if (result.Succeeded)
                    RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View();
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.TryLogin(vm.Email, vm.Password);
                if (result == SignInResult.Success)
                    return Redirect(returnUrl ?? "/");
                ModelState.AddModelError(nameof(LoginViewModel.Email),
                    "Invalid email or password");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}