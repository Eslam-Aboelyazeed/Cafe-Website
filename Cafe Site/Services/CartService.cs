using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Cafe_Site.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository repository;
        private readonly IDefaultRepository<Order> orderRepository;

        public CartService(ICartRepository repository, IDefaultRepository<Order> OrderRepository)
        {
            this.repository = repository;
            orderRepository = OrderRepository;
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
                Product_Price = Math.Round(p.Price, 2),
                Product_Size = (p.Size == 'S') ? "Small" : (p.Size == 'M') ? "Medium" : "Large",
                Quantity = p.Quantity
            }).ToList();
        }

        public void DeleteFromCart(int oid, int pid)
        {
            repository.Delete(repository.GetElement(op => op.Order_Id == oid && op.Product_Id == pid, null));

            repository.SaveChanges();
        }

        public void Checkout(Dictionary<string, string> data)
        {
            foreach (var item in data.Keys)
            {
                bool flag;
                int value;

                flag = int.TryParse(item, out value);

                if (flag)
                {
                    var product = repository.GetElement(
                        op => op.Product_Id == value && op.Order_Id == int.Parse(data["OID"]) && op.Size == char.Parse(data[$"s{item}"]), null);

                    if (product != null)
                    {
                        product.Quantity = int.Parse(data[item]);

                        repository.Update(product);
                    }
                }
                //if (item[0]. == Type.)
                //{
                //    // code to update the order total price

                //}
                //else if ((!item.StartsWith('s')) && (!item.StartsWith('p')) && item != "OID" && item != "Total")
                //{

                //}
            }

            var order = orderRepository.GetElement(o => o.Order_Id == int.Parse(data["OID"]), null);

            order.Order_Status = 'D';
            order.Order_TotalPrice = decimal.Parse(data["Total"]);

            orderRepository.Update(order);

            repository.SaveChanges();
        }
    }
}
