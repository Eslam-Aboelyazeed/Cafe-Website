using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.ViewModels;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Drawing;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cafe_Site.Services
{
    public class ProductService : IProductService
    {
        private readonly IDefaultRepository<Product> repository;
        private readonly IDefaultRepository<Product_Size_Price> psrepository;
        private readonly IDefaultService defaultService;

        //private readonly UserManager<ApplicationUser> urepo;

        //private readonly IDefaultRepository<aspnet> urepo;

        public ProductService(IDefaultRepository<Product> repository, IDefaultRepository<Product_Size_Price> psrepository, IDefaultService defaultService)
        {
            this.repository = repository;
            this.psrepository = psrepository;
            this.defaultService = defaultService;
            //this.urepo = urepo;
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
                //Product_Image = p.Product_Image,
                userId = p.userId
            }).ToList();

            return products;
        }

        //public byte[] ImageToByteArray(string path)
        //{
        //    try
        //    {
        //        Image imageIn = Image.FromFile(path);

        //        using (var ms = new MemoryStream())
        //        {
        //            imageIn.Save(ms, imageIn.RawFormat);
        //            return ms.ToArray();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new byte[0];
        //    }

        //}

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
                SPrice = (product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'S') != null) ? product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'S').Price.ToString("0.00") : "0",  //"-",
                MPrice = (product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'M') != null) ? product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'M').Price.ToString("0.00") : "0",  //"-",
                LPrice = (product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'L') != null) ? product.Product_Size_Prices.FirstOrDefault(ps => ps.Product_Id == product.Product_Id && ps.Size == 'L').Price.ToString("0.00") : "0",  //"-",
                //Product_Image = Convert.ToBase64String(product.Product_Image),
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

        public void DeleteProduct(int id)
        {
            var product = repository.GetElement(p => p.Product_Id == id, null);

            if (product != null)
            {
                repository.Delete(product);
            }

            var smallSizedProduct = psrepository.GetElement(ps => ps.Product_Id == id && ps.Size == 'S', null);

            if(smallSizedProduct != null)
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
        }

        public void DeleteSize(int id, char size)
        {
            var SizedProduct = psrepository.GetElement(ps => ps.Product_Id == id && ps.Size == size, null);

            if (SizedProduct != null)
            {
                psrepository.Delete(SizedProduct);

                repository.SaveChanges();
            }
        }

        //public ApplicationUser GetUser(string userId)
        //{
        //    return urepo.Users.FirstOrDefault(u => u.Id == userId);
        //}
    }
}
