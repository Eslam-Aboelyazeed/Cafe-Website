using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.ViewModels;
using System.Security.Claims;

namespace Cafe_Site.Services
{
    public interface IProductService
    {
        public DashboardViewModel GetProductsForAdminIndex(int page, int pageSize, string filter, string type);
        public DashboardViewModel GetProductsForAdminGetProducts(int page, int pageSize, string filter, string type);
        public List<ProductInfoViewModel> GetAllProducts();
        public List<ProductInfoViewModel> GetProductsWithoutAddtions();
		public List<ProductInfoViewModel> GetProductsWithFilterWithoutAddtions(string filter);
        public List<ProductInfoViewModel> GetProductsWithFilter(string filter);
        public List<ProductInfoViewModel> GetAdditionsWithFilter(string filter);
        public List<ProductInfoViewModel> GetAllAdditions();
        public ProductInfoViewModel GetProduct(int id);
        public void InsertProduct(ProductInfoViewModel productInfo, string uid);
        public void UpdateProduct(ProductInfoViewModel productInfo);
        public bool DeleteProduct(int id);
        public bool DeleteSize(int id, char size);
        //public ApplicationUser GetUser(string userId);
        public List<OrderHistoryViewModel> GetOrderHistory(int pid);


	}
}