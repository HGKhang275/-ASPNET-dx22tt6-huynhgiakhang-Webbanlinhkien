using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProTechTiveGear.Models;
namespace ProTechTiveGear.Controllers
{

	public class AdminController : Controller
	{
		// GET: Admin
		ProTechTiveGearEntities db = new ProTechTiveGearEntities();
		public ActionResult SignOut()
		{
			//FormsAuthentication.SignOut();
			Response.Cookies.Clear();
			return RedirectToAction("Login", "Admin");

		}

		public ActionResult Login()
		{
			return View();

		}
		[HttpPost]
		public ActionResult Login(FormCollection collection)
		{


			var userName = collection["userName"];

			var passWord = collection["passWord"];


			Admin ad = db.Admins.SingleOrDefault(n => n.Username == userName && n.Passwords == passWord);
			if (ad != null)
			{
				Session["Account"] = ad;
				Response.Cookies["usr"].Value = ad.Username;

				var name = db.Admins.SingleOrDefault(a => a.Username == ad.Username).Name;
				Response.Cookies["Name"].Value = name;

				var atar = db.Admins.SingleOrDefault(a => a.Username == ad.Username).Picture;
				if (atar == null || atar == "")
				{
					atar = "~/img/Item/avatar-default-icon.png";
				}

				Response.Cookies["avatar"].Value = atar;

				return RedirectToAction("Index", "Items");
			}
			else

				ModelState.AddModelError("", "The user login or password  is incorrect..");

			return View();



		}
	}
}


		
		

		

	