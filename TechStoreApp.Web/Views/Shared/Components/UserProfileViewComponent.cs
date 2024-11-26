﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechStoreApp.Data;
using TechStoreApp.Data.Models;
using TechStoreApp.Data.Repository.Interfaces;
using TechStoreApp.Web.ViewModels.Header;
using TechStoreApp.Web.ViewModels.User;

namespace TechStoreApp.Web.Views.Shared.Components
{
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly IRepository<ApplicationUser, Guid> userRepository;

        public UserProfileViewComponent(IRepository<ApplicationUser, Guid> _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userRepository.GetAllAttached()
                .Where(u => u.Id == userId)
                .Select(u => new ProfileViewModel()
                {
                    ProfilePictureUrl = u.ProfilePictureUrl!
                })
                .FirstOrDefaultAsync();

            var userModel = new UserModel()
            {
                User = user
            };

            return View("UserProfile", userModel);
        }

    }
}
