using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Razor;
using System.Domain.Entities.Security;
using System.Domain.Entities.Security.Parameters;
using System.Application.Business.Security;

namespace Web.Controllers.Home
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult ErrorSession()
        {
            return View();
        }

    }
}