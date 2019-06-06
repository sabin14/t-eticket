using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using prabhuEticket.AppCode;
using prabhuEticket.AppCode;
using prabhuEticket.Models;
using prabhuEticket.Repository;

namespace prabhuEticket.Controllers
{
    [SessionCheck]
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(CustomerProfile_request request)
        {
            CustomerProfile_output output = new CustomerProfile_output();
            if (ModelState.IsValid)
            {
                CustomerRepo _repo = new CustomerRepo();
                output = _repo.getCustomerProfile(request, Session["token_id"].ToString());
                ViewBag.detail = output.data;
                return Json(output);
            }
            return Json(request);
        }
    }
}