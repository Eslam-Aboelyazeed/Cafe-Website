using Cafe_Site.Models;
using Cafe_Site.Services;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace Cafe_Site.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IProductService _productService;

		public HomeController(ILogger<HomeController> logger, IProductService productService)
		{
			_logger = logger;
			_productService = productService;
		}

		public IActionResult Index()
		{
			List<ProductInfoViewModel> products = _productService.GetAllProducts();

			products = products.Where(p => p.Product_Quantity > 0).ToList();
			// Get 4 random products
			var random = new Random();
			var randomProducts = products.OrderBy(p => random.Next()).Take(4).ToList();

			// Create a new MenuViewModel with the random products
			var viewModel = new MenuViewModel
			{
				Products = randomProducts
			};

			return View(viewModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
