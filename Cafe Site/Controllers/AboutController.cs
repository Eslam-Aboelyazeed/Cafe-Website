using Microsoft.AspNetCore.Mvc;

namespace Cafe_Site.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
