using Cafe_Site.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cafe_Site.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        public IActionResult Index()
        {
            var products = cartService.GetProducts(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            return View("Index", products);
        }

        public IActionResult RemoveFromCart(int oid, int pid)
        {
            cartService.DeleteFromCart(oid, pid);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Checkout([FromBody] Dictionary<string, string> data)
        {
            //var d = data;

            cartService.Checkout(data);

            return Json(new { success = true });
        }
    }
}
