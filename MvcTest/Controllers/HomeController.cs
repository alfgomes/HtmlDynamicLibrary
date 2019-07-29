using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTest.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "This library is designed to make web forms development easier by rendering components with less cshtml information and more use of metadata attributes.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Contact page.";

			return View();
		}
	}
}