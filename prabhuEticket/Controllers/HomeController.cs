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
    public class HomeController : Controller
    {
        public HomeController()
        {
            //if (Session["user"] == null || Session["token_id"] == null)
            //{
            //    RedirectToAction("Index","Login");
            //}
        }
        httpConnect connect = new httpConnect();
        JsonObject JsonObject = new JsonObject();
        public ActionResult Index()
        {
            report_request request = new report_request();
            report_output output = new report_output();
            ReportRepo _reportRepo = new ReportRepo();
            output=_reportRepo.getTransactionReport(request, Session["token_id"].ToString());
            if (output.status == false || output.status_code != 200)
            {
                ViewBag.detail = new report_output.Data();
            }
            else
            {
                ViewBag.detail = output.data;
            }
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}