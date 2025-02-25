﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreApp.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<string>? ImageUrls {  get; set; } = new List<string>();
    }
}
