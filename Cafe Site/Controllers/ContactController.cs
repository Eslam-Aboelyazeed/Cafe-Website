using Cafe_Site.ViewModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;

namespace Cafe_Site.Controllers
{
    public class ContactController : Controller
    {

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("itiexaminationsystem@gmail.com");
                mail.To.Add("itiexaminationsystem@gmail.com");
                mail.Subject = "Message from " + contact.Email;
                mail.Body = contact.Message;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("itiexaminationsystem@gmail.com", "9:48*5/3/2024");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                ViewBag.Message = "Email sent successfully!";

                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error sending email: " + ex.Message;
                //ViewBag.Message = ex.Message.ToString();

            }

            return View();
        }
    }
}