﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TechStoreApp.Data.Models;
using TechStoreApp.Data.Repository.Interfaces;
using TechStoreApp.Services.Data.Interfaces;
using TechStoreApp.Web.ViewModels.Products;
using TechStoreApp.Web.ViewModels.Search;

namespace TechStoreApp.Services.Data
{
    public class SearchService : ISearchService
    {
        private readonly IRepository<Product, int> productRepository; 
        private readonly IRepository<Favorited, object> favoritedRepository;
        private readonly IRepository<Category, int> categoryRepository;
        private readonly IUserService userService;
        public SearchService(IRepository<Product, int> _productRepository,
            IRepository<Favorited, object> _favoriteRepository,
            IRepository<Category, int> _categoryRepository,
            IUserService _userService)
        {
            productRepository = _productRepository;
            favoritedRepository = _favoriteRepository;
            categoryRepository = _categoryRepository;
            userService = _userService;
        }
        public async Task<SearchViewModel> GetSearchViewModel(SearchViewModel model)
        {
            int currentPage = model.CurrentPage;
            string category = model.Category;
            string orderBy = model.Orderby;
            string query = model.Query;
            int pageSize = 12;

            var userId = userService.GetUserId();

            var productsQuery = productRepository.GetAllAttached()
                .Include(p => p.Favorites)
                .Include(p => p.Category)
                .AsQueryable();

            if (model.CategoryId != 0)
            {
                var temp = categoryRepository.GetById(model.CategoryId);

                category = temp == null ? category : temp.Description!;
            }

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                productsQuery = productsQuery
                    .Where(p => p.Category.Description == category);
            }

            if (!string.IsNullOrEmpty(query))
            {
                productsQuery = productsQuery
                    .Where(p => p.Name.ToLower()
                    .Contains(query.ToLower()));
            }

            int totalItems = productsQuery.Count();

            if (orderBy == "default")
            {

            } else if (orderBy == "priceAsc")
            {
                productsQuery = productsQuery
                    .OrderBy(r => r.Price);
            } else if (orderBy == "priceDesc")
            {
                productsQuery = productsQuery
                    .OrderByDescending(r => r.Price);
            } else if (orderBy == "likesDesc")
            {
                productsQuery = productsQuery
                    .OrderByDescending(r => r.Favorites.Count);
            }

            List<ProductViewModel> results = await productsQuery
               .Select(p => new ProductViewModel
               {
                    ProductId = p.ProductId,
                    TotalLikes = p.Favorites.Count,
                    CategoryId = p.CategoryId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    ImageUrl = p.ImageUrl,
                    CheckedString = p.Favorites
                        .Any(p => p.UserId == userId) ? "checked" : "unchecked"
               })
               .Skip((currentPage - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            model.Query = query;
            model.Products = results;
            model.CurrentPage = currentPage;
            model.TotalPages = totalPages;
            
            return model;
        }
    }
}
