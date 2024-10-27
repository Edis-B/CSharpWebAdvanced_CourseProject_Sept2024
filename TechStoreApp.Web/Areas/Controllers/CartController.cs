﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Versioning;
using System.Security.Claims;
using TechStoreApp.Data;
using TechStoreApp.Data.Models;
using TechStoreApp.Services.Data.Interfaces;
using TechStoreApp.Web.ViewModels;
using TechStoreApp.Web.ViewModels.Cart;
using TechStoreApp.Web.ViewModels.Products;

namespace TechStoreApp.Web.Areas.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly TechStoreDbContext context;
        private readonly ICartService cartService;

        public CartController(TechStoreDbContext _context, ICartService _cartService)
        {
            cartService = _cartService;
            context = _context;
        }
        
        [HttpPost]
        public async Task<JsonResult> AddToCart([FromBody] AddToCartViewModel model)
        {
            return await cartService.AddToCartAsync(model);
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var usersCartItems = await cartService.GetCartItemsAsync();

            return View(usersCartItems);
        }
        [HttpPut]
        public async Task<JsonResult> IncreaseCount([FromBody] CartFormModel model)
        {
            return await cartService.IncreaseCountAsync(model);
        }

        [HttpPut]
        public async Task<JsonResult> DecreaseCount([FromBody] CartFormModel model)
        {
            return await cartService.DecreaseCountAsync(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetCartItemsCount()
        {
            return await cartService.GetCartItemsCountAsync();
        }
        [HttpDelete]
        public async Task<JsonResult> RemoveFromCart([FromBody] RemoveFromCartViewModel model)
        {
            var userId = GetUserId();
            var cartItem = await context.CartItems
                .Where(ci => ci.ProductId == model.ProductId)
                .Where(ci => ci.Cart.UserId == userId)
                .FirstOrDefaultAsync();

            context.CartItems.Remove(cartItem);
            await context.SaveChangesAsync();

            return Json(new { success = true, message = "Successfully removed item from cart!" });
        }
        [HttpDelete]
        public async Task<JsonResult> ClearCart()
        {
            var userId = GetUserId();

            var cart = await context.Carts
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return Json(new { success = false, message = "Cart is already empty!" });
            }

            context.Remove(cart);
            await context.SaveChangesAsync();

            return Json(new { success = true, message = "Successfully removed item from cart!" });
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}