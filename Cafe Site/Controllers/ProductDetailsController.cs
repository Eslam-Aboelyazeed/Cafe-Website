using Microsoft.AspNetCore.Mvc;

namespace Cafe_Site.Controllers
{
    public class ProductDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View("ProductDetails");
        }
    }
}
