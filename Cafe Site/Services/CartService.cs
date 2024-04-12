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
        private readonly IDefaultRepository<Product> pRepository;

        public CartService(ICartRepository repository, IDefaultRepository<Order> OrderRepository, IDefaultRepository<Product> pRepository)
        {
            this.repository = repository;
            orderRepository = OrderRepository;
            this.pRepository = pRepository;
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
                Quantity = p.Quantity,
                MaxQuantity = p.product.Product_Quantity
            }).ToList();
        }

        public bool DeleteFromCart(int oid, int pid, char psize)
        {
            try
            {
                repository.Delete(repository.GetElement(op => op.Order_Id == oid && op.Product_Id == pid && op.Size == psize, null));

                repository.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

		public bool Checkout(Dictionary<string, string> data)
        {
            foreach (var i in data.Keys)
            {
                bool flag;
                int value;
                char size = i[i.Length - 1];
                string item = i.Remove(i.Length - 1);

                flag = int.TryParse(item, out value);

                if (flag)
                {
                    var oproduct = repository.GetElement(
                        op => op.Product_Id == value && op.Order_Id == int.Parse(data["OID"]) && op.Size == size, null);

                    if (oproduct != null)
                    {
                        oproduct.Quantity = int.Parse(data[i]);

                        repository.Update(oproduct);

                        var product = pRepository.GetElement(p => p.Product_Id == value, null);
                        
                        if(product != null)
                        {
                            if ((product.Product_Quantity - int.Parse(data[i])) < 0)
                            {
                                return false;
                            }

                            product.Product_Quantity -= int.Parse(data[i]);

                            pRepository.Update(product);
                        }
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

            return true;

		}
    }
}
