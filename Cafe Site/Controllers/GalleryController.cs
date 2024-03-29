using Microsoft.AspNetCore.Mvc;

namespace Cafe_Site.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
