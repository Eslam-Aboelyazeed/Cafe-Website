using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.ViewModels;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Drawing;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing.Printing;

namespace Cafe_Site.Services
{
    public class ProductService : IProductService
    {
        private readonly IDefaultRepository<Product> repository;
        private readonly IDefaultRepository<Product_Size_Price> psrepository;
        private readonly IDefaultService defaultService;
		private readonly ICartRepository crepository;

        public ProductService(IDefaultRepository<Product> repository, IDefaultRepository<Product_Size_Price> psrepository, IDefaultService defaultService, ICartRepository crepository)
        {
            this.repository = repository;
            this.psrepository = psrepository;
            this.defaultService = defaultService;
			this.crepository = crepository;
        }

        public DashboardViewModel GetProductsForAdminIndex(int page, int pageSize, string filter, string type)
        {
            List<ProductInfoViewModel> menuItems = GetAllProducts();
            var count = menuItems.Count;
            var items = menuItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new DashboardViewModel
            {
                Products = items,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Filter = filter,
                Type = type
            };

            return viewModel;
        }

        public DashboardViewModel GetProductsForAdminGetProducts(int page, int pageSize, string filter, string type)
        {
            List<ProductInfoViewModel> menuItems;

            if (filter.Contains("All") && type == "Product")
            {
                menuItems = GetProductsWithoutAddtions();
            }
            else if (type == "Product")
            {
                menuItems = GetProductsWithFilter(filter);
            }
            else if (filter.Contains("All") && type == "Addition")
            {
                menuItems = GetAllAdditions();
            }
            else if (type == "Addition")
            {
                menuItems = GetAdditionsWithFilter(filter);
            }
            else
            {
                menuItems = GetAllProducts();
            }

            var count = menuItems.Count;
            var items = menuItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new DashboardViewModel
            {
                Products = items,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Filter = filter,
                Type = type
            };

            return viewModel;
        }

        public List<ProductInfoViewModel> GetAllProducts()

        {
            byte[] defaultByteArray = new byte[0];

            var products = repository.GetAll("Product_Size_Prices").Select(p => new ProductInfoViewModel() 
            { 
                Product_Id = p.Product_Id,
                Product_Name = p.Product_Name,
                Product_Type = p.Product_Type,
                Product_Quantity = p.Product_Quantity,
                Product_Description = p.Product_Description,
                SPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S').Price.ToString("0.00") : "-",
                MPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M').Price.ToString("0.00") : "-",
                LPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L').Price.ToString("0.00") : "-",
                Product_Image = Convert.ToBase64String(p.Product_Image?? defaultByteArray),
                userId = p.userId
            }).ToList();

            return products;
        }

        public List<ProductInfoViewModel> GetProductsWithoutAddtions()
        {
			byte[] defaultByteArray = new byte[0];

			var products = repository.GetElementsByFilter(p => !p.Product_Type.StartsWith("Add-"), "Product_Size_Prices")?.Select(p => new ProductInfoViewModel()
			{
				Product_Id = p.Product_Id,
				Product_Name = p.Product_Name,
				Product_Type = p.Product_Type,
				Product_Quantity = p.Product_Quantity,
				Product_Description = p.Product_Description,
				SPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S').Price.ToString("0.00") : "-",
				MPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M').Price.ToString("0.00") : "-",
				LPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L').Price.ToString("0.00") : "-",
				Product_Image = Convert.ToBase64String(p.Product_Image ?? defaultByteArray),
				userId = p.userId
			}).ToList();

			return products;
		}

		public List<ProductInfoViewModel> GetProductsWithFilterWithoutAddtions(string filter)
		{
			byte[] defaultByteArray = new byte[0];

			var products = repository.GetElementsByFilter(p => p.Product_Type == filter && !p.Product_Type.StartsWith("Add-"), "Product_Size_Prices")?.Select(p => new ProductInfoViewModel()
			{
				Product_Id = p.Product_Id,
				Product_Name = p.Product_Name,
				Product_Type = p.Product_Type,
				Product_Quantity = p.Product_Quantity,
				Product_Description = p.Product_Description,
				SPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S').Price.ToString("0.00") : "-",
				MPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M').Price.ToString("0.00") : "-",
				LPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L').Price.ToString("0.00") : "-",
				Product_Image = Convert.ToBase64String(p.Product_Image ?? defaultByteArray),
				userId = p.userId
			}).ToList();

			return products;
		}

        public List<ProductInfoViewModel> GetProductsWithFilter(string filter)
        {
            byte[] defaultByteArray = new byte[0];

            var products = repository.GetElementsByFilter(p => p.Product_Type == filter, "Product_Size_Prices")?.Select(p => new ProductInfoViewModel()
            {
                Product_Id = p.Product_Id,
                Product_Name = p.Product_Name,
                Product_Type = p.Product_Type,
                Product_Quantity = p.Product_Quantity,
                Product_Description = p.Product_Description,
                SPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S').Price.ToString("0.00") : "-",
                MPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M').Price.ToString("0.00") : "-",
                LPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L').Price.ToString("0.00") : "-",
                Product_Image = Convert.ToBase64String(p.Product_Image ?? defaultByteArray),
                userId = p.userId
            }).ToList();

            return products;
        }

        public List<ProductInfoViewModel> GetAdditionsWithFilter(string filter)
        {
            byte[] defaultByteArray = new byte[0];

            var products = repository.GetElementsByFilter(p => p.Product_Type.StartsWith($"{filter}"), "Product_Size_Prices")?.Select(p => new ProductInfoViewModel()
            {
                Product_Id = p.Product_Id,
                Product_Name = p.Product_Name,
                Product_Type = p.Product_Type,
                Product_Quantity = p.Product_Quantity,
                Product_Description = p.Product_Description,
                SPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S').Price.ToString("0.00") : "-",
                MPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M').Price.ToString("0.00") : "-",
                LPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L').Price.ToString("0.00") : "-",
                Product_Image = Convert.ToBase64String(p.Product_Image ?? defaultByteArray),
                userId = p.userId
            }).ToList();

            return products;
        }

        public List<ProductInfoViewModel> GetAllAdditions()
        {
            byte[] defaultByteArray = new byte[0];

            var products = repository.GetElementsByFilter(p => p.Product_Type.StartsWith("Add-"), "Product_Size_Prices")?.Select(p => new ProductInfoViewModel()
            {
                Product_Id = p.Product_Id,
                Product_Name = p.Product_Name,
                Product_Type = p.Product_Type,
                Product_Quantity = p.Product_Quantity,
                Product_Description = p.Product_Description,
                SPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'S').Price.ToString("0.00") : "-",
                MPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'M').Price.ToString("0.00") : "-",
                LPrice = (p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L') != null) ? p.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == p.Product_Id && ps.Size == 'L').Price.ToString("0.00") : "-",
                Product_Image = Convert.ToBase64String(p.Product_Image ?? defaultByteArray),
                userId = p.userId
            }).ToList();

            return products;
        }

        public void InsertProduct(ProductInfoViewModel productInfo, string uid)
        {
            var ProductImage = defaultService.ImageToByteArray(productInfo.Product_Image);

            repository.Insert(new Product()
            {
                Product_Name = productInfo.Product_Name,
                Product_Type = productInfo.Product_Type,
                Product_Quantity = productInfo.Product_Quantity,
                Product_Image = ProductImage,
                Product_Description = productInfo.Product_Description,

                userId = uid
            });

            repository.SaveChanges();

            var pid = repository.GetElement(
                p => p.Product_Name == productInfo.Product_Name &&
                p.Product_Quantity == productInfo.Product_Quantity &&
                p.Product_Type == productInfo.Product_Type &&
                p.Product_Description == productInfo.Product_Description && p.userId == uid, null).Product_Id;

            decimal value;
            bool flag = decimal.TryParse(productInfo.SPrice, out value); 

            if(flag && value > 0)
            {
                psrepository.Insert(new Product_Size_Price()
                {
                    Product_Id = pid,
                    Size = 'S',
                    Price = Math.Round(value,2)
                });
            }

            flag = decimal.TryParse(productInfo.MPrice, out value);

            if(flag && value > 0)
            {
                psrepository.Insert(new Product_Size_Price()
                {
                    Product_Id = pid,
                    Size = 'M',
                    Price = Math.Round(value, 2)
                });
            }

            flag = decimal.TryParse(productInfo.LPrice, out value);

            if(flag && value > 0)
            {
                psrepository.Insert(new Product_Size_Price()
                {
                    Product_Id = pid,
                    Size = 'L',
                    Price = Math.Round(value, 2)
                });
            }

            repository.SaveChanges();
        }

        public ProductInfoViewModel GetProduct(int id)
        {
            var product = repository.GetElement(p => p.Product_Id == id, "Product_Size_Prices");

            var productvm = new ProductInfoViewModel()
            {
                Product_Id = product.Product_Id,
                Product_Name = product.Product_Name,
                Product_Type = product.Product_Type,
                Product_Quantity = product.Product_Quantity,
                Product_Description = product.Product_Description,
                SPrice = (product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'S') != null) ? product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'S').Price.ToString("0.00") : "0", 
                MPrice = (product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'M') != null) ? product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'M').Price.ToString("0.00") : "0", 
                LPrice = (product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'L') != null) ? product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'L').Price.ToString("0.00") : "0", 
                userId = product.userId
            };

            return productvm;
        }

        public void UpdateProduct(ProductInfoViewModel productInfo)
        {
            var product = repository.GetElement(p => p.Product_Id == productInfo.Product_Id, null);

            var ProductImage = defaultService.ImageToByteArray(productInfo.Product_Image);

            product.Product_Name = productInfo.Product_Name;
            product.Product_Quantity = productInfo.Product_Quantity;
            product.Product_Type = productInfo.Product_Type;
            if(ProductImage.Length != 0)
            {
                product.Product_Image = ProductImage;
            }
            product.Product_Description = productInfo.Product_Description;
            product.userId = productInfo.userId;

            repository.Update(product);

            decimal value;
            bool flag = decimal.TryParse(productInfo.SPrice, out value);

            if (flag && value > 0)
            {
                var smallSizedProduct = psrepository.GetElement(ps => ps.Product_Id == productInfo.Product_Id && ps.Size == 'S', null);
                if(smallSizedProduct != null)
                {
                    smallSizedProduct.Price = Math.Round(value, 2);
                    psrepository.Update(smallSizedProduct);
                }
                else
                {
                    psrepository.Insert(new Product_Size_Price()
                    {
                        Product_Id = productInfo.Product_Id,
                        Size = 'S',
                        Price = Math.Round(value, 2)
                    });
                }
            }
            else
            {
                var smallSizedProduct = psrepository.GetElement(ps => ps.Product_Id == productInfo.Product_Id && ps.Size == 'S', null);
                if (smallSizedProduct != null)
                {
                    psrepository.Delete(smallSizedProduct);
                }
            }

            flag = decimal.TryParse(productInfo.MPrice, out value);

            if (flag && value > 0)
            {
                var mediumSizedProduct = psrepository.GetElement(ps => ps.Product_Id == productInfo.Product_Id && ps.Size == 'M', null);
                if(mediumSizedProduct != null)
                {
                    mediumSizedProduct.Price = Math.Round(value, 2);
                    psrepository.Update(mediumSizedProduct);
                }
                else
                {
                    psrepository.Insert(new Product_Size_Price()
                    {
                        Product_Id = productInfo.Product_Id,
                        Size = 'M',
                        Price = Math.Round(value, 2)
                    });
                }
            }
            else
            {
                var mediumSizedProduct = psrepository.GetElement(ps => ps.Product_Id == productInfo.Product_Id && ps.Size == 'M', null);
                if (mediumSizedProduct != null)
                {
                    psrepository.Delete(mediumSizedProduct);
                }
            }

            flag = decimal.TryParse(productInfo.LPrice, out value);

            if (flag && value > 0)
            {
                var largeSizedProduct = psrepository.GetElement(ps => ps.Product_Id == productInfo.Product_Id && ps.Size == 'L', null);
                if(largeSizedProduct != null)
                {
                    largeSizedProduct.Price = Math.Round(value, 2);
                    psrepository.Update(largeSizedProduct);
                }
                else
                {
                    psrepository.Insert(new Product_Size_Price()
                    {
                        Product_Id = productInfo.Product_Id,
                        Size = 'L',
                        Price = Math.Round(value, 2)
                    });
                }
            }
            else
            {
                var largeSizedProduct = psrepository.GetElement(ps => ps.Product_Id == productInfo.Product_Id && ps.Size == 'L', null);
                if (largeSizedProduct != null)
                {
                    psrepository.Delete(largeSizedProduct);
                }
            }

            repository.SaveChanges();
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = repository.GetElement(p => p.Product_Id == id, null);

                if (product != null)
                {
                    repository.Delete(product);
                }

                var smallSizedProduct = psrepository.GetElement(ps => ps.Product_Id == id && ps.Size == 'S', null);

                if (smallSizedProduct != null)
                {
                    psrepository.Delete(smallSizedProduct);
                }

                var MediumSizedProduct = psrepository.GetElement(ps => ps.Product_Id == id && ps.Size == 'M', null);

                if (MediumSizedProduct != null)
                {
                    psrepository.Delete(MediumSizedProduct);
                }

                var LargeSizedProduct = psrepository.GetElement(ps => ps.Product_Id == id && ps.Size == 'L', null);

                if (LargeSizedProduct != null)
                {
                    psrepository.Delete(LargeSizedProduct);
                }

                repository.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteSize(int id, char size)
        {
            try
            {
                var SizedProduct = psrepository.GetElement(ps => ps.Product_Id == id && ps.Size == size, null);

                if (SizedProduct != null)
                {
                    psrepository.Delete(SizedProduct);

                    repository.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

		public List<OrderHistoryViewModel> GetOrderHistory(int pid)
		{
            try
            {
                var item = repository.GetElement(p => p.Product_Id == pid, null)?.Product_Type;
                if (item.StartsWith("Add-"))
                {
                    var orders = crepository.GetAllByTwoIncludesAndFilter("product", "order", op => op.Product_Id == pid && op.order.Order_Status == 'D').ToList();

                    var totalQuantity = crepository.GetAllByTwoIncludesAndFilter("product", "order", op => op.order.Order_Status == 'D' && op.product.Product_Type.StartsWith("Add-"))?.Select(op => op.Quantity).ToList();

                    int totalCount = 0;

                    foreach (var i in totalQuantity)
                    {
                        totalCount += i;
                    }

                    var proQuantity = crepository.GetAllByTwoIncludesAndFilter("order", "product", op => op.Product_Id == pid && op.order.Order_Status == 'D').Select(op => op.Quantity).ToList();

                    var proCount = 0;

                    foreach (var i in proQuantity)
                    {
                        proCount += i;
                    }

                    decimal proPer = Math.Round((decimal)proCount / totalCount * 100);

                    return orders.Select(o => new OrderHistoryViewModel
                    {
                        Product_Name = o.product.Product_Name,
                        Order_Date = o.order.Order_Date,
                        Size = (o.Size == 'S') ? "Small" : (o.Size == 'M') ? "Medium" : "Large",
                        Quantity = o.Quantity,
                        UnitPrice = o.Price.ToString("0.00"),
                        TotalPrice = (o.Price * o.Quantity).ToString("0.00"),
                        Product_Percentage = (int)proPer,
                        Small_Product_Percentage = 0,
                        Medium_Product_Percentage = (int)proPer,
                        Large_Product_Percentage = 0,
                        Product_Type = o.product.Product_Type
                    }).ToList();
                }
                else
                {
                    var orders = crepository.GetAllByTwoIncludesAndFilter("product", "order", op => op.Product_Id == pid && op.order.Order_Status == 'D').ToList();

                    var totalQuantity = crepository.GetAllByTwoIncludesAndFilter("product", "order", op => op.order.Order_Status == 'D' && !op.product.Product_Type.StartsWith("Add-"))?.Select(op => op.Quantity).ToList();

                    int totalCount = 0;

                    foreach (var i in totalQuantity)
                    {
                        totalCount += i;
                    }

                    var proQuantity = crepository.GetAllByTwoIncludesAndFilter("order", "product", op => op.Product_Id == pid && op.order.Order_Status == 'D').Select(op => op.Quantity).ToList();

                    var proCount = 0;

                    foreach (var i in proQuantity)
                    {
                        proCount += i;
                    }

                    decimal proPer = Math.Round((decimal)proCount / totalCount * 100);

                    var sproQuantity = crepository.GetAllByTwoIncludesAndFilter("order", "product", op => op.Product_Id == pid && op.order.Order_Status == 'D' && op.Size == 'S').Select(op => op.Quantity).ToList();

                    var sproCount = 0;

                    foreach (var i in sproQuantity)
                    {
                        sproCount += i;
                    }

                    decimal sproPer = Math.Round((decimal)sproCount / proCount * 100);

                    var mproQuantity = crepository.GetAllByTwoIncludesAndFilter("order", "product", op => op.Product_Id == pid && op.order.Order_Status == 'D' && op.Size == 'M').Select(op => op.Quantity).ToList();

                    var mproCount = 0;

                    foreach (var i in mproQuantity)
                    {
                        mproCount += i;
                    }

                    decimal mproPer = Math.Round((decimal)mproCount / proCount * 100);

                    var lproQuantity = crepository.GetAllByTwoIncludesAndFilter("order", "product", op => op.Product_Id == pid && op.order.Order_Status == 'D' && op.Size == 'L').Select(op => op.Quantity).ToList();

                    var lproCount = 0;

                    foreach (var i in lproQuantity)
                    {
                        lproCount += i;
                    }

                    decimal lproPer = Math.Round((decimal)lproCount / proCount * 100);

                    return orders.Select(o => new OrderHistoryViewModel
                    {
                        Product_Name = o.product.Product_Name,
                        Order_Date = o.order.Order_Date,
                        Size = (o.Size == 'S') ? "Small" : (o.Size == 'M') ? "Medium" : "Large",
                        Quantity = o.Quantity,
                        UnitPrice = o.Price.ToString("0.00"),
                        TotalPrice = (o.Price * o.Quantity).ToString("0.00"),
                        Product_Percentage = (int)proPer,
                        Small_Product_Percentage = (int)sproPer,
                        Medium_Product_Percentage = (int)mproPer,
                        Large_Product_Percentage = (int)lproPer,
                        Product_Type = o.product.Product_Type
                    }).ToList();
                }

            }
            catch (Exception)
            {
                return new List<OrderHistoryViewModel>();
            }

		}
	}
}
