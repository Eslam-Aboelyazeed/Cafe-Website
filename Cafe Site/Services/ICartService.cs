using Cafe_Site.ViewModels;

namespace Cafe_Site.Services
{
    public interface ICartService
    {
        public List<CartViewModel> GetProducts(string uid);

		public bool DeleteFromCart(int oid, int pid, char psize);


		public bool Checkout(Dictionary<string, string> data);
	}
}