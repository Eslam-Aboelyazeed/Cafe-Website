using Cafe_Site.Models;
using Cafe_Site.Repository;
using Microsoft.AspNetCore.Diagnostics;

namespace Cafe_Site.Services
{
    public class ProductDetailsService :IProductDetailsService
    {
        private readonly IDefaultRepository<Product> prodRepo;
        private readonly IDefaultRepository<Product_Reviews> reviewRepo;
        private readonly IDefaultRepository<Product_Size_Price> sizeRepo;
        private readonly IDefaultRepository<Order> orderRepo;
        private readonly IDefaultRepository<Order_Products> orderProdRepo;
        public ProductDetailsService(IDefaultRepository<Product> prod, IDefaultRepository<Product_Reviews>reviews, IDefaultRepository<Product_Size_Price> sizes , IDefaultRepository<Order> order, IDefaultRepository<Order_Products> orderProd)
        {                                                                                                                                                        
            prodRepo=prod;
            reviewRepo=reviews;
            sizeRepo=sizes;
            orderRepo=order;
            orderProdRepo=orderProd;
        }
        public List<Product> GetAddProducts(string productType)
        {
            return prodRepo.GetElementsByFilter(p=> p.Product_Type == $"Add-{productType}",null).ToList();

        }
        public List<Product_Reviews> GetProductReviews(int productID)
        {
            return reviewRepo.GetElementsByFilter(p => p.Product_Id == productID, null).ToList();
        }

        public List<Product_Size_Price> GetProductSizes(int productID)
        {
            return sizeRepo.GetElementsByFilter(p=>p.Product_Id == productID,null).ToList();
        }
        public Product Getproduct(int productID)
        {
            return prodRepo.GetElement(p=>p.Product_Id==productID,null);
        }
        public string addReview(int rate, string review, int id,string user, Product p)
        {
            if (review == null || rate == 0)
            {
                return "empty";
            }
            
            Product_Reviews? oldReview = reviewRepo.GetElement(p => p.User_Name == user && p.Product_Id == id, null);
            List<Order_Products> order_Products = orderProdRepo.GetElementsByFilter(p => p.order.userId == user && p.order.Order_Status == 'D' && p.Product_Id == id, "order");
            if (oldReview == null)
            {
                Product_Reviews rev = new Product_Reviews()
                {
                    Product_Id = id,
                    Product_Rate = rate,
                    Product_Review = review,
                    User_Name = user,
                    product = p
                };
                reviewRepo.Insert(rev);
                reviewRepo.SaveChanges();
                return "Your review is added successfully!";
            }
            else if(order_Products.Count()== 0)
            {
                return "You didn't order this product before.";
            }
            else
            {
                return "You added a review to the same product before.";
            }
            

          
            
        }

        public Order? GetOrder(string userID)
        {
            return orderRepo.GetElement(p=> p.userId== userID && p.Order_Status=='C',null);
        }

        public bool addProductToOrder(int productID, char size, List<int>adds, string userId)
        {
            Order? cartOrder = GetOrder(userId);
            if (cartOrder == null)
            {
                cartOrder = new Order()
                {
                    Order_Date = DateTime.Now,
                    Order_Status = 'C',
                    Order_TotalPrice = 0,
                    userId = userId

                };
                orderRepo.Insert(cartOrder);
                orderRepo.SaveChanges();
            }
            Order_Products? orderProduct = orderProdRepo.GetElement(p => p.Product_Id == productID && p.Size == size && p.Order_Id==cartOrder.Order_Id, "product");
            if (orderProduct == null)
            {
                  orderProduct = new Order_Products()
                {
                    Product_Id = productID,
                    Order_Id = cartOrder.Order_Id,
                    Price = Math.Round( sizeRepo.GetElement(p => p.Product_Id == productID && p.Size == size, null).Price,2),
                    Quantity = 1,
                    Size = size
                };
                orderProdRepo.Insert(orderProduct);

            }
            else
            {
                if (orderProduct.product.Product_Quantity > orderProduct.Quantity)
                {
                    orderProduct.Quantity++;
                }
                else
                    return false;
            }
            foreach (int add in adds)
            {
                Product addprod = prodRepo.GetElement(p => p.Product_Id == add,null);
                Order_Products? addition = orderProdRepo.GetElementsByFilter(p => p.Product_Id == add && p.Order_Id== cartOrder.Order_Id, null).FirstOrDefault();

                if (addition == null)
                {
                    addition = new Order_Products()
                    {
                        Order_Id = orderProduct.Order_Id,
                        Product_Id = addprod.Product_Id,
                        Price = Math.Round( sizeRepo.GetElement(p=>p.Product_Id==addprod.Product_Id && p.Size=='M',null).Price,2) ,
                        Quantity = 1,
                        Size = 'M'

                    };
                    orderProdRepo.Insert(addition);
                }else
                {
                    if(addprod.Product_Quantity > addition.Quantity)
                    {
                        addition.Quantity++;
                    }
                }
            }
            orderProdRepo.SaveChanges();
            return true;
        }
    }

}
