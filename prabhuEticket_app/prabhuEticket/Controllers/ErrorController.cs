using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prabhuEticket.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            if (Request.Form["responseCode"] != null)
            {
                ViewBag.response_code = Request.Form["responseCode"].ToString();
                ViewBag.exception= Request.Form["exception"];
            }
            else
            {
                ViewBag.response_code = "404";
                Exception ex = Server.GetLastError();
                ViewBag.exception = ex;
            }

            
            return View();
        }
    }
}