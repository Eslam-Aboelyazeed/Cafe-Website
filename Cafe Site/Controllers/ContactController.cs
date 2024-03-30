using Cafe_Site.Models;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;

namespace Cafe_Site.Controllers
{
	public class ContactController : Controller
	{

		private readonly CafeSiteContext cafeSiteContext;

		public ContactController(CafeSiteContext cafeSiteContext)
		{
			this.cafeSiteContext = cafeSiteContext;
		}
		public ActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Contact(ContactViewModels model)
		{
			CafeSiteContext db = new CafeSiteContext();

			if (ModelState.IsValid)
			{

				try
				{
					var contact = new Contact
					{
						Name = model.Name,
						Email = model.Email,
						Message = model.Message,
						CreatedAt = DateTime.Now
					};

					// Add contact to database
					db.Contact.Add(contact);
					db.SaveChanges();

					ViewBag.Message = "Message sent successfully!";
				}
				catch (Exception ex)
				{
					ViewBag.Message = "Error occurred while sending message.";
					return View(model);
				}

			}
			return View(model);
		}
	}
}