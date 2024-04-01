using Cafe_Site.Models;
using Cafe_Site.Services;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Site.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IProductDetailsService productDetailsServ;
        private readonly IDefaultService defaultService;
        Product product;
        static int id=0;
        public ProductDetailsController(IProductDetailsService productDetails, IDefaultService Service)
        {
            productDetailsServ=productDetails;
            defaultService=Service;
        }
        public IActionResult Index(int Id)
        {
            id = Id;
            product = productDetailsServ.Getproduct(id);
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
        [Authorize]
        public IActionResult review(int rate, string review)
        {
            
            var name= User.Claims.FirstOrDefault()?.Value;
            productDetailsServ.addReview(rate, review, id, User.Claims.FirstOrDefault()?.Value,product);
            
            return RedirectToAction("Index",id);
        }
        [Authorize]
        public void addProductToCart(int product, char size, List<int> productAdds)
        {
            var userId = User.Claims.FirstOrDefault()?.Value;
            var prodID = product;
            var s = size;
            var adds= productAdds;
            productDetailsServ.addProductToOrder(prodID, s, adds, userId);
        }
    }
}
