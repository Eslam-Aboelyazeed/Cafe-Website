namespace Cafe_Site.ViewModels
{
    public class DashboardViewModel
    {
        public List<ProductInfoViewModel> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Filter { get; set; } = "All";
        public string Type { get; set; } = "All";
    }
}
