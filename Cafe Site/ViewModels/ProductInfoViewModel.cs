using System.ComponentModel.DataAnnotations;

namespace Cafe_Site.ViewModels
{
    public class ProductInfoViewModel
    {
        public int Product_Id { get; set; }
        [Required(ErrorMessage = "The Product Name is Required")]
        public string Product_Name { get; set; }
        [Required(ErrorMessage = "The Product Type is Required")]
        public string Product_Type { get; set; }
        [RegularExpression("^([a-zA-Z]:\\\\)?(?:[^\\\\/:*?\"<>|\\r\\n]+\\\\)*[^\\\\/:*?\"<>|\\r\\n]+\\.(?:jpg|jpeg|png|gif|bmp)(?![^\\\\/:*?\"<>|\\r\\n]*\")$", ErrorMessage = "Plaese Enter The Image Full Path")]
        public string? Product_Image { get; set; }
        //[Required(ErrorMessage = "The Product Quantity is Required")]
        [Range(1, 100, ErrorMessage = "The Product Quantity Must be Higher Than 0 and Lower Than 100")]
        public int Product_Quantity { get; set; }
        [Required(ErrorMessage = "The Product Description is Required")]
        public string Product_Description { get; set; }
        [DataType(DataType.Currency)]
        public string? SPrice { get; set; }
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "The Product Medium Size Price is Required")]
        [Range(1, double.MaxValue, ErrorMessage = "The Product Medium Size Price Must be Higher Than 0")]
        public string MPrice { get; set; }
        [DataType(DataType.Currency)]
        public string? LPrice { get; set; }
        public string? userId { get; set; }
    }
}
