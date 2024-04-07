
namespace Cafe_Site.ViewModels
{
	public class OrderHistoryViewModel
	{
        public string Product_Name { get; set; }
        public DateTime Order_Date { get; set; }
		public string Size { get; set; }
		public int Quantity { get; set; }
		public string UnitPrice { get; set; }
		public string TotalPrice { get; set; }
        public int Product_Percentage { get; set; }
        public int Small_Product_Percentage { get; set; }
        public int Medium_Product_Percentage { get; set; }
        public int Large_Product_Percentage { get; set; }
    }
}
