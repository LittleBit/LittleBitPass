using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBitPass.Models.Connectors;

namespace LittleBitPass.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
			var test = new LdapConnector ();

            return View();
        }
    }
}