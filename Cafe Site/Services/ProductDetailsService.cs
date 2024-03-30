
using Cafe_Site.Models;
using Cafe_Site.Repository;

namespace Cafe_Site.Services
{
    public class ProductDetailsService :IProductDetailsService
    {
        private readonly IDefaultRepository<Product> prodRepo;
        private readonly IDefaultRepository<Product_Reviews> reviewRepo;
        private readonly IDefaultRepository<Product_Size_Price> sizeRepo;
        public ProductDetailsService(IDefaultRepository<Product> prod, IDefaultRepository<Product_Reviews>reviews, IDefaultRepository<Product_Size_Price> sizes )
        {
            prodRepo=prod;
            reviewRepo=reviews;
            sizeRepo=sizes;
        }
        public List<Product> GetAddProducts(string productType)
        {
            return prodRepo.GetElementsByFilter(p=> p.Product_Type == $"Add-{productType}",null).ToList();

        }
        public List<Product_Reviews> GetProductReviews(int productID)
        {
            return reviewRepo.GetElementsByFilter(p => p.Product_Id == productID, null).ToList();
        }

        public List<Product_Size_Price> GetProductSizes(int productID)
        {
            return sizeRepo.GetElementsByFilter(p=>p.Product_Id == productID,null).ToList();
        }
        public Product Getproduct(int productID)
        {
            return prodRepo.GetElement(p=>p.Product_Id==productID,null);
        }
        public void addReview(int rate, string review)
        {
            
        }
    }
}
