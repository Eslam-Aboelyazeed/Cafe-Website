namespace Cafe_Site.ViewModels
{
    public class MenuViewModel
    {
        public List<ProductInfoViewModel> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
