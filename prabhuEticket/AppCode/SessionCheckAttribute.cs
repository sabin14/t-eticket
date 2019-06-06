using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace prabhuEticket.AppCode
{
    public class SessionCheckAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName=filterContext.RouteData.Values["controller"].ToString();
            if (controllerName.ToLower() != "login")
            {
                HttpSessionStateBase Session = filterContext.HttpContext.Session;
                if (Session != null && (Session["user"] == null || Session["token_id"] == null))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Login" },
                                { "Action", "Index" }
                                    });
                }
            }
            else {
                HttpSessionStateBase Session = filterContext.HttpContext.Session;
                if (Session != null && (Session["user"] != null && Session["token_id"] != null))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Index" }
                                    });
                }
            }
        }
    }
}