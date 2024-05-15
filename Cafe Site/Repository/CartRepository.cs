using Cafe_Site.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Site.Repository
{
    public class CartRepository : DefaultRepository<Order_Products>, ICartRepository
    {
        public CartRepository(CafeSiteContext db): base(db) { }
        

        public List<Order_Products> GetAllByTwoIncludesAndFilter(string? include1, string? include2, Func<Order_Products, bool> func)
        {
            try
            {
                return db.Order_Products.Include(include1).Include(include2).Where(func).ToList();
            }
            catch (Exception)
            {
                return db.Order_Products.ToList();
            }
        }
    }
}
