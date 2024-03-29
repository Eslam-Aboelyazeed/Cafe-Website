using Cafe_Site.Models;

namespace Cafe_Site.ViewModels
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }
        public List<Product> AddProducts { get; set; }
        public List<Product_Reviews> productReviews { get; set; }

        public List<Product_Size_Price> productSizePrices { get; set; }
    }
}
