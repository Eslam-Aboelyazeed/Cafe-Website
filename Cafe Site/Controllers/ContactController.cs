using Cafe_Site.Models;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Net;
using System.Net.Mail;

namespace Cafe_Site.Controllers
{
	public class ContactController : Controller
	{

		private readonly CafeSiteContext db;

		public ContactController(CafeSiteContext cafeSiteContext)
		{
			this.db = cafeSiteContext;
		}
		public ActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Contact(ContactViewModels model)
		{
			
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
					db.Add(contact);
					db.SaveChanges();

					ViewBag.Message = "Message sent successfully!";

					return View("Contact",new ContactViewModels()
					{
						flag = true,
						Name = "",
						Email = "",
						Message = ""
					});
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
