using Cafe_Site.ViewModels;

namespace Cafe_Site.Services
{
    public interface ICartService
    {
        public List<CartViewModel> GetProducts(string uid);
    }
}