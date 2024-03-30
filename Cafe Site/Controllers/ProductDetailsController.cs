using Cafe_Site.Models;
using Cafe_Site.Services;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Site.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IProductDetailsService productDetailsServ;
        public ProductDetailsController(IProductDetailsService productDetails)
        {
            productDetailsServ=productDetails;
        }
        public IActionResult Index()
        {

            int id = 12;
            Product product = productDetailsServ.Getproduct(id);
            string type=product.Product_Type;
            List<Product> products = productDetailsServ.GetAddProducts(type);
            List<Product_Reviews> productReview = productDetailsServ.GetProductReviews(id);
            List<Product_Size_Price> productSizes = productDetailsServ.GetProductSizes(id);
            ProductDetailsModel viewModel = new ProductDetailsModel()
            {   
                AddProducts=products,
                productReviews=productReview,
                productSizePrices=productSizes,
                Product=product
            };
            return View("ProductDetails", viewModel);
        }
        public IActionResult review(int rate, string review)
        {

            return RedirectToAction("Index");
        }

        public void addProductToCart()
        { }
    }
}
