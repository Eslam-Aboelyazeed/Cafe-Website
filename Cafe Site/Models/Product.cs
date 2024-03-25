using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Site.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Type { get; set; }
       
        public byte[]? Product_Image { get; set; }
        public int Product_Quantity { get; set; }
        public string Product_Description { get; set; }

        [ForeignKey("user")]
        public string userId { get; set; }

        public ApplicationUser user { get; set; }

        public List<Order_Products> Order_Products { get; set; }
        public List<Product_Size_Price> Product_Size_Prices { get; set; }
        public List<Product_Reviews> Product_Reviews { get; set; }
    }
}
