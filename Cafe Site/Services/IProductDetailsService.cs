using Cafe_Site.Models;

namespace Cafe_Site.Services
{
    public interface IProductDetailsService
    {
        public List<Product> GetAddProducts(string productType);
        public List<Product_Reviews> GetProductReviews(int productID);
        public List<Product_Size_Price> GetProductSizes(int productID);
        public Product Getproduct(int productID);
        public string addReview(int rate, string review, int id, string user, Product p );
        public Order? GetOrder(string userID);
        public bool addProductToOrder(int productID, char size, List<int> adds, string userId);
    }
}
