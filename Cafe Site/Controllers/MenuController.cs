using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.Services;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cafe_Site.Controllers
{
    public class MenuController : Controller
    {
        private readonly IProductService _productService;

        public MenuController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int page = 1, int pageSize = 4)
        {
            List<ProductInfoViewModel> menuItems = _productService.GetAllProducts();

            var count = menuItems.Count;
            var items = menuItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new MenuViewModel
            {
                Products = items,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            //if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //{
            //    return PartialView("_ProductsPartial", viewModel);
            //}

            return View(viewModel);
        }

        public IActionResult GetProducts(int page = 1, int pageSize = 4, string filter = "All")
        {
            List<ProductInfoViewModel> menuItems;

			if (filter == "All")
            {
				menuItems = _productService.GetAllProducts();
			}
            else
            {
				menuItems = _productService.GetProductsByFilter(filter);
			}

			var count = menuItems.Count;
            var items = menuItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new MenuViewModel
            {
                Products = items,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };


            return PartialView("_ProductsPartial", viewModel);
            
        }

    }

}
