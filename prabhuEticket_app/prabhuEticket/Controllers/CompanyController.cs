using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prabhuEticket.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateCompany()
        {
            return View();
        }
        public ActionResult CreateUser()
        {

            return View();
        }
    }
}