using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using LittleBitPass.Models;

namespace LittleBitPass.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
			ViewBag.Title = "Home";
			ViewBag.ImageUrl = ImageOfTheDay.Instance.Image;
			ViewBag.ImageCopyright = ImageOfTheDay.Instance.ImageCopyright;
			return View();
		}
	}
}

