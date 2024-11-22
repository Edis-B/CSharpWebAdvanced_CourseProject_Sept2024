﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStoreApp.Services.Data.Interfaces;
using static TechStoreApp.Common.GeneralConstraints;

namespace TechStoreApp.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        public AccountController(IUserService _userService,
            IProfileService _profileService)
        {
            userService = _userService;
            profileService = _profileService;
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> Manage(string? userName, string? email, int page = 1, int itemsPerPage = 12)
        {
            var model = await profileService.GetAllUsersAsync(userName, email, page, itemsPerPage);

            return View("Manage", model);
        }
    }
}
