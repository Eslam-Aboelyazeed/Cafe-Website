using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Site.Models
{
    public class Product_Reviews
    {
        [ForeignKey("product")]
        public int Product_Id { get; set; }
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public int Product_Rate { get; set; }
        public string? Product_Review { get; set; }
        public Product product { get; set; }
    }
}
