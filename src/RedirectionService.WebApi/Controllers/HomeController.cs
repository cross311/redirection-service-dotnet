using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedirectionService.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Json(new {version = "1.0.0", service = "redirection", status = "ok"}, JsonRequestBehavior.AllowGet);
        }
    }
}
