using Microsoft.AspNetCore.Mvc;

namespace Cafe_Site.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
