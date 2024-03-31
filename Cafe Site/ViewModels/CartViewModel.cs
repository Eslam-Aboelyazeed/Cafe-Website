using Cafe_Site.Models;

namespace Cafe_Site.ViewModels
{
    public class CartViewModel
    {
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string? Product_Image { get; set; }
        public int Quantity { get; set; }
        public decimal? Product_Price { get; set; }
        public string? Product_Size { get; set; }
        //public List<Order_Products> Order_Products { get; set; }

    }
}
