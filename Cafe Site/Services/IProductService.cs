using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.ViewModels;

namespace Cafe_Site.Services
{
    public interface IProductService
    {
        public List<ProductInfoViewModel> GetAllProducts();
        public ProductInfoViewModel GetProduct(int id);
        public void InsertProduct(ProductInfoViewModel productInfo);
        public void UpdateProduct(ProductInfoViewModel productInfo);
        public void DeleteProduct(int id);
    }
}