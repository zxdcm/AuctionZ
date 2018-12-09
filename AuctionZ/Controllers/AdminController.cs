using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using AuctionZ.Models;
using AuctionZ.Models.MappingProfiles;
using AuctionZ.Models.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using static AuctionZ.Utils.Utils;

namespace AuctionZ.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {

        private readonly ILotsService _lotsService;
        private readonly IRoleService _roleService;
        private readonly IUserServices _userServices;

        public AdminController(ILotsService lotService, IRoleService roleService,
            IUserServices userService)
        {
            _lotsService = lotService;
            _roleService = roleService;
            _userServices = userService;
        }

        public IActionResult Index() => View("Categories");

        public IActionResult Categories() => View();

        public IActionResult Lots(int? category, string title, int page = 1, 
            bool? active = null, string username=null)
        {
            int pageSize = 4;
            var criteria = new LotsFilterCriteria()
                { Page = page, Category = category, Title = title,
                  PageSize = pageSize, Active = active, UserId = GetUserIdByUserName(username)};
            var lots = _lotsService.GetAllLotsWithUsers(criteria).ToVm();
            var model = new LotsViewModel()
            {
                Lots = lots,
                Pagination = new PageViewModel(_lotsService.GetLotsCount(criteria), page, pageSize),
                Filter = new FilterViewModel(GetCategories(), title, category, active, username)
            };
            return View(model);
        }

        public IActionResult Roles() => View(_roleService.GetRoles());

        private IEnumerable<CategoryDto> GetCategories()
        {
            var categoryService = HttpContext.RequestServices.GetRequiredService<ICategoryService>();
            return categoryService.GetItems();
        }

        public async Task<IActionResult> CreateRole([Required]string name)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.CreateRoleAsync(name);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Roles));                
                AddErrorsFromResult(result);
            }
            return View(name);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role != null)
            {
                var result = await _roleService.DeleteRoleAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Roles));
                AddErrorsFromResult(result);
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View(nameof(Roles), _roleService.GetRoles());
        }

        public async Task<IActionResult> EditRole(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            var members = new List<UserDto>();
            var nonMembers = new List<UserDto>();
            foreach (var user in _userServices.GetItems())
            {
                var list = await _userServices.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleViewModel
                { Role = role,  Members = members, NonMembers = nonMembers });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleModificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (int userId in model.IdsToAdd ?? new int[]{})
                {
                    var user = _userServices.GetItem(userId);
                    if (user != null)
                    {
                        var result = await _userServices.AddToRoleAsync(user,model.RoleName);
                        if (!result.Succeeded)
                            AddErrorsFromResult(result);
                    }
                }
                foreach (var userId in model.IdsToDelete ?? new int[] { })
                {
                    var user = _userServices.GetItem(userId);
                    if (user != null)
                    {
                        var result = await _userServices.RemoveFromRoleAsync(user,model.RoleName);
                        if (!result.Succeeded)
                            AddErrorsFromResult(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Roles));

            return await EditRole(model.RoleId);

        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

    }
}