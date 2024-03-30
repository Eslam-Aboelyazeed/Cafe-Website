using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Cafe_Site.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public DateTime Order_Date { get; set;}
        [Column(TypeName = "money")]
        public decimal? Order_TotalPrice { get; set;}
        public char Order_Status { get; set; }

        [ForeignKey("user")]
        public string userId { get; set; }

        public ApplicationUser user { get; set; }

        public List<Order_Products> Order_Products { get; set; }
    }
}
