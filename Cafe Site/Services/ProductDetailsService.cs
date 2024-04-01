
using Cafe_Site.Models;
using Cafe_Site.Repository;

namespace Cafe_Site.Services
{
    public class ProductDetailsService :IProductDetailsService
    {
        private readonly IDefaultRepository<Product> prodRepo;
        private readonly IDefaultRepository<Product_Reviews> reviewRepo;
        private readonly IDefaultRepository<Product_Size_Price> sizeRepo;
        private readonly IDefaultRepository<Order> orderRepo;
        private readonly IDefaultRepository<Order_Products> orderProdRepo;
        public ProductDetailsService(IDefaultRepository<Product> prod, IDefaultRepository<Product_Reviews>reviews, IDefaultRepository<Product_Size_Price> sizes )
        {
            prodRepo=prod;
            reviewRepo=reviews;
            sizeRepo=sizes;
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
        public void addReview(int rate, string review, int id,string user, Product p)
        {
            Product_Reviews rev = new Product_Reviews()
            {
               Product_Id=id,
               Product_Rate=rate,
               Product_Review=review,
               User_Name=user,
               product=p
            };
            reviewRepo.Insert(rev);
            reviewRepo.SaveChanges();
        }

        public Order? GetOrder(string userID)
        {
            return orderRepo.GetElementsByFilter(p=> p.userId== userID && p.Order_Status=='C',null).FirstOrDefault();
        }

        public void addProductToOrder(int productID, char size, List<int>adds, string userId)
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
            Order_Products? orderProduct = orderProdRepo.GetElement(p => p.Product_Id == productID && p.Size == size && p.Order_Id==cartOrder.Order_Id, null);
            if (orderProduct == null)
            {
                Order_Products op = new Order_Products()
                {
                    Product_Id = productID,
                    Order_Id = cartOrder.Order_Id,
                    Price = sizeRepo.GetElement(p => p.Product_Id == productID && p.Size == size, null).Price,
                    Quantity = 1,
                    Size = size
                };
                orderProdRepo.Insert(op);

            }
            foreach (int add in adds)
            {
                Product addprod = prodRepo.GetElement(p => p.Product_Id == add,null);
                Order_Products? addition = orderProdRepo.GetElementsByFilter(p => p.Product_Id == add && p.Order_Id== cartOrder.Order_Id, null).FirstOrDefault();

                if (addition == null)
                {
                    Order_Products Oproduct = new Order_Products()
                    {
                        Order_Id = orderProduct.Order_Id,
                        Product_Id = addprod.Product_Id,
                        Price = addprod.Product_Size_Prices[0].Price,
                        Quantity = 1,
                        Size = 'M'

                    };
                    orderProdRepo.Insert(Oproduct);
                }
            }
            orderProdRepo.SaveChanges();
        }
    }

}
