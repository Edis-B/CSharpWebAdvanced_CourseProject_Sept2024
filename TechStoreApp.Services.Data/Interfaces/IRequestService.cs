﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreApp.Services.Data.Interfaces
{
    public interface IRequestService 
    {
        bool IsAjaxRequest(HttpRequest request);
        Task<TType> GetProductIdFromRequest<TType>(HttpRequest request);
    }
}