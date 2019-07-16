using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace prabhuEticket
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Response.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", "/Error");
            sb.AppendFormat("<input type='hidden' name='responseCode' value='404'>");
            sb.AppendFormat("<input type='hidden' name='exception' value=\"{0}\">",exception.Message);
            // Other params go here
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            if (exception != null)
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            try
            {
                // This is to stop a problem where we were seeing "gibberish" in the
                // chrome and firefox browsers
                HttpApplication app = sender as HttpApplication;
                app.Response.Filter = null;
            }
            catch
            {
            }
        }
    }
}
