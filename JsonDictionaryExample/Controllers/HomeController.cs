using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsonDictionaryExample.Extensions;

namespace JsonDictionaryExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [HttpPost]
        public JsonResult Submit([ModelBinder(typeof(TypedDictionaryModelBinder))]Dictionary<string, int> stats)
        {
            if (ModelState.IsValid)
            {
                return Json(new { Dictionary = stats });
            }
            else
            {
                return Json(new { Error = "Oops" });
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
