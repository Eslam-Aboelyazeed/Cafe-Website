using System.ComponentModel.DataAnnotations;

namespace Cafe_Site.ViewModels
{
    public class ContactViewModels
    {
		[Required(ErrorMessage ="Please Input Your Name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Please Input Your Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please Input Your Message")]
		public string Message { get; set; }
		public bool flag { get; set; } = false;
	}
}
