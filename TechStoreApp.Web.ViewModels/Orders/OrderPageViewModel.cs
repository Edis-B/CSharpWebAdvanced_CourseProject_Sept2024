﻿using TechStoreApp.Data.Models;
using TechStoreApp.Web.ViewModels.Address;
using TechStoreApp.Web.ViewModels.Cart;

namespace TechStoreApp.Web.ViewModels.Orders
{
    public class OrderPageViewModel
    {
        public decimal TotalCost { get; set; }
        public AddressFormModel Address { get; set; }
        public List<AddressViewModel> AllUserAddresses { get; set; } = new List<AddressViewModel>();
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
    }
}