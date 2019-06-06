using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prabhuEticket.AppCode;
using prabhuEticket.Models;
using prabhuEticket.Repository;

namespace prabhuEticket.Controllers
{
    [SessionCheck]
    public class LoginController : Controller
    {
        public LoginController()
        {
            //if (Session["user"] != null && Session["token_id"] == null)
            //{
            //    RedirectToAction("Index", "Home");
            //}
        }
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Index(authenticate_request request)
        {
            authenticate_output output = new authenticate_output();
            if (String.IsNullOrEmpty(request.username) == false && string.IsNullOrEmpty(request.password) == false)
            {
                AuthenticationRepo _auth = new AuthenticationRepo();
                output=_auth.generateToken(request);
                if (output.status == false || output.status_code != 200)
                {
                    ModelState.AddModelError("validationSummary",output.message);
                    ViewBag.validationSummary = output.message;
                }
            }
            if (ModelState.IsValid)
            {
                Session["user"] = request.username;
                Session["token_id"] = output.data.token;
                Session["data"] = output.data;
                Session["name"] = output.data.userInfo.name;
                Session["img"] = output.data.userInfo.image;
                return RedirectToAction("Index", "Home");
            }
            return View(request);
        }
    }
}
