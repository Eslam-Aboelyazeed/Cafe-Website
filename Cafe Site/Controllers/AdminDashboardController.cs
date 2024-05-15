using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.Services;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cafe_Site.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
	{
        private readonly IProductService productService;

        public AdminDashboardController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index(int page = 1, int pageSize = 5, string filter = "All", string type = "All")
        {
            var viewModel = productService.GetProductsForAdminIndex(page, pageSize, filter, type);

            return View(viewModel);
        }

        public IActionResult GetProducts(int page = 1, int pageSize = 5, string filter = "All", string type = "All")
        {
            var viewModel = productService.GetProductsForAdminGetProducts(page, pageSize, filter, type);

            return PartialView("_DashboardProductsPartial", viewModel);
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
                productService.InsertProduct(product, User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

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
            var result = productService.DeleteProduct(id);

            return Json(new { success = result });
        }

        public IActionResult DeleteSize(int id, char size)
        {
            var result = productService.DeleteSize(id, size);

            return Json(new { success = result });
        }

        public IActionResult GetOrdersHistory(int id)
        {
            var orders = productService.GetOrderHistory(id);

            return View("GetOrdersHistory", orders);
        }
    }
}
