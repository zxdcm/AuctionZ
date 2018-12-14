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
using Microsoft.Extensions.Logging;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AuctionZ.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserServices _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserServices userServices,
                                 ILogger<AccountController> logger)
        {
            _userService = userServices;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userDto = vm.ToDto();
                var result = await _userService.TryRegister(userDto, vm.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }

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