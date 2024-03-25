using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Site.Models
{
    public class Product_Size_Price
    {
        [ForeignKey("product")]
        public int Product_Id { get; set; }
        public char Size { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Product product { get; set; }
    }
}
