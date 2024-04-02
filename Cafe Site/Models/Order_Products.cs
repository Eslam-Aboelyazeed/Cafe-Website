using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Site.Models
{
    public class Order_Products
    {
        [ForeignKey("order")]
        public int Order_Id { get; set; }

        [ForeignKey("product")]
        public int Product_Id { get; set; }

        public char Size { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        //[Column(TypeName = "money")]
        //public decimal UnitPrice { get; set; }
        //[Column(TypeName = "money")]
        //public decimal TotalPrice { get; set; }
        public Order order { get; set; }
        public Product product { get; set; }
    }
}
