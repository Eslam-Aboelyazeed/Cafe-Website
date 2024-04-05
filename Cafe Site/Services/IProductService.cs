using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.ViewModels;
using System.Security.Claims;

namespace Cafe_Site.Services
{
    public interface IProductService
    {
        public List<ProductInfoViewModel> GetAllProducts();
        public ProductInfoViewModel GetProduct(int id);
        public void InsertProduct(ProductInfoViewModel productInfo, string uid);
        public void UpdateProduct(ProductInfoViewModel productInfo);
        public bool DeleteProduct(int id);
        public bool DeleteSize(int id, char size);
        //public ApplicationUser GetUser(string userId);
        public List<OrderHistoryViewModel> GetOrderHistory(int pid);


	}
}