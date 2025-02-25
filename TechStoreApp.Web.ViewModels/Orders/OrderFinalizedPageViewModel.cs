﻿using TechStoreApp.Web.ViewModels.Address;

namespace TechStoreApp.Web.ViewModels.Orders
{
    public class OrderFinalizedPageViewModel
    {
        public decimal TotalSum { get; set; }
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }

        public CartViewModel Cart { get; set; }
        public AddressFormModel Address { get; set; }
    }
}
