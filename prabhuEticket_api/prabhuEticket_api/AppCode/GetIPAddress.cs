using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace prabhuEticket_api.AppCode
{
    public class GetIPAddress
    {
        public string getIPAdd(HttpRequestMessage requestMessage)
        {
            // Web Hosting
            if (requestMessage.Properties.ContainsKey("MS_HttpContext") || requestMessage.Properties.ContainsKey("MS_IsLocal"))
            {
                //string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                //return HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : null;
                if (((HttpContextBase)requestMessage.Properties["MS_HttpContext"]).Request.UserHostAddress != null &&
                    ((HttpContextBase)requestMessage.Properties["MS_HttpContext"]).Request.UserHostAddress != "")
                {
                    //return HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : null;
                    return ((HttpContextBase)requestMessage.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
                else if (((HttpContextBase)requestMessage.Properties["MS_IsLocal"]).Request.UserHostAddress != null &&
                    ((HttpContextBase)requestMessage.Properties["MS_IsLocal"]).Request.UserHostAddress != "")
                {
                    return ((HttpContextBase)requestMessage.Properties["MS_IsLocal"]).Request.UserHostAddress;
                }
            }
            return "";
        }
    }
    public static class GetIP
    {
        public static string GetIPAddress(HttpRequestMessage requestMessage)
        {
            // Web Hosting
            if (requestMessage.Properties.ContainsKey("MS_HttpContext") || requestMessage.Properties.ContainsKey("MS_IsLocal"))
            {
                //string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                //return HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : null;
                if (((HttpContextBase)requestMessage.Properties["MS_HttpContext"]).Request.UserHostAddress != null &&
                    ((HttpContextBase)requestMessage.Properties["MS_HttpContext"]).Request.UserHostAddress != "")
                {
                    //return HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : null;
                    return ((HttpContextBase)requestMessage.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
                else if (((HttpContextBase)requestMessage.Properties["MS_IsLocal"]).Request.UserHostAddress != null &&
                    ((HttpContextBase)requestMessage.Properties["MS_IsLocal"]).Request.UserHostAddress != "")
                {
                    return ((HttpContextBase)requestMessage.Properties["MS_IsLocal"]).Request.UserHostAddress;
                }
            }
            return null;
        }
        public static bool AllowIP(HttpRequestMessage request)
        {
            var whiteListedIPs = ConfigurationManager.AppSettings["WhiteListedIPAddresses"];
            if (!string.IsNullOrEmpty(whiteListedIPs))
            {
                var whiteListIPList = whiteListedIPs.Split(',').ToList();
                //var ipAddressString = request.GetIPAddress();
                var ipAddressString = GetIPAddress(request);
                var ipAddress = IPAddress.Parse(ipAddressString);
                var isInwhiteListIPList =
                    whiteListIPList
                        .Where(a => a.Trim()
                        .Equals(ipAddressString, StringComparison.InvariantCultureIgnoreCase))
                        .Any();
                return isInwhiteListIPList;
            }
            return true;
        }
    }
    public class IPFilterHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool allowIp = prabhuEticket_api.AppCode.GetIP.AllowIP(request);
            if (allowIp == true)
            {
                return await base.SendAsync(request, cancellationToken);
            }
            //if (request.AllowIP())
            //{
            //    return await base.SendAsync(request, cancellationToken);
            //}
            return request
                .CreateErrorResponse(HttpStatusCode.Unauthorized
                    , "IP not allowed. Not authorized to view/access this resource");
        }
    }
    public class CheckIPAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool allowIp = prabhuEticket_api.AppCode.GetIP.AllowIP(actionContext.Request);
            if (allowIp == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                HttpStatusCode.Unauthorized, "IP not allowed. Not authorized to view/access this resource");
            }
        }
    }
}