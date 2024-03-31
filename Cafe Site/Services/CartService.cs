using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Cafe_Site.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository repository;

        public CartService(ICartRepository repository)
        {
            this.repository = repository;
        }
        public List<CartViewModel> GetProducts(string uid)
        {
            var products = repository.GetAllByTwoIncludesAndFilter("order", "product", op => op.order.userId == uid && op.order.Order_Status == 'C');

            return products.Select(p => new CartViewModel
            {
                Order_Id = p.Order_Id,
                Product_Id = p.Product_Id,
                Product_Image = Convert.ToBase64String(p.product.Product_Image?? new byte[0]),
                Product_Name = p.product.Product_Name,
                Product_Price = p.Price,
                Product_Size = (p.Size == 'S') ? "Small" : (p.Size == 'M') ? "Medium" : "Large",
                Quantity = p.Quantity
            }).ToList();
        }
    }
}
