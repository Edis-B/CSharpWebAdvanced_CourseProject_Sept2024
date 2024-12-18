﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStoreApp.Services.Data.Interfaces;
using static TechStoreApp.Common.GeneralConstraints;
namespace TechStoreApp.Web.Views.Shared.Components
{
    [Authorize(Roles = AdminRoleName)]
    [ViewComponent]
    public class UserOrdersAdmin : ViewComponent
    {
        private readonly IOrderService orderService;
        public UserOrdersAdmin(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var model = await orderService.GetUserOrdersListViewModelAsync(userId);

            return View("UserOrdersAdmin", model);
        }
    }
}
