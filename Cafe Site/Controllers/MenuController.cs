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

        public IActionResult Index()
        {
            List<ProductInfoViewModel> menuItems = _productService.GetAllProducts();
            return View(menuItems);
        }
    }
}
