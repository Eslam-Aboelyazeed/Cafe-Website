using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.Services;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Site.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
	{
        private readonly IProductService productService;

        public AdminDashboardController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
		{
            var products = productService.GetAllProducts();

            return View("Index",products);
		}

        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(ProductInfoViewModel product)
        {
            if (ModelState.IsValid)
            {
                productService.InsertProduct(product);

                return RedirectToAction("Index");
            }

            return View("Add", product);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = productService.GetProduct(id);

            return View("Update", product);
        }

        [HttpPost]
        public IActionResult Update(ProductInfoViewModel product)
        {
            if (ModelState.IsValid)
            {
                productService.UpdateProduct(product);

                return RedirectToAction("Index");
            }

            return View("Update", product);
        }

        public IActionResult Delete(int id)
        {
            productService.DeleteProduct(id);

            return RedirectToAction("Index");
        }
    }
}
