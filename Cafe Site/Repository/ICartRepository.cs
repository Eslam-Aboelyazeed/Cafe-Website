using Cafe_Site.Models;

namespace Cafe_Site.Repository
{
    public interface ICartRepository
    {
        public List<Order_Products> GetAllByTwoIncludesAndFilter(string? include1, string? include2, Func<Order_Products, bool> func);
    }
}