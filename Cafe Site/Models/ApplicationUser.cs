using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cafe_Site.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string User_Fname { get; set; }
        public string User_Lname { get; set; }
        public string User_Address { get; set; }    
        public List<Order>? orders { get; set; }
        public List<Product>? products { get; set; }

    }
}
