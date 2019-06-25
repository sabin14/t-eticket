using prabhuEticket_api.AppCode;
using prabhuEticket_api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace prabhuEticket_api.Repository
{
    public class AuthenticationRepo
    {
        CheckAPIKey aPIKey = new CheckAPIKey();
        globFunction func = new globFunction();
        public authenticate_output validate_login(authenticate_request request, string api_key)
        {
            authenticate_output allObj = new authenticate_output();
            GetIPAddress getIp = new GetIPAddress();
            HttpRequestMessage httpRequestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            var ipAddr = getIp.getIPAdd(httpRequestMessage);
            var ipAdd = IPAddress.Parse(ipAddr);
            string location = globFunction.GetUserCountryByIp(ipAdd.ToString());
            DataTable sql = func.RunSQL("spa_login @flag='i',@access_token=" + func.singleQuote("") + ",@api_key=" + func.singleQuote(api_key) +
                            ",@location_trace=" + func.singleQuote(location) + ",@ip_address=" + func.singleQuote(ipAdd.ToString())+",@user_name="+func.singleQuote(request.username)+
                            ",@password="+func.singleQuote(request.password));
            string code = sql.Rows[0]["code"].ToString();
            if (code == "0")
                allObj = aPIKey.fetchLoginRows(sql.Rows[0]);
            else
            {
                allObj.error_Lists = new List<error_list>();
                foreach (DataRow rows in sql.Rows)
                {
                    error_list error = new error_list();
                    error.error_code = code;
                    error.error_message = rows["message"].ToString();
                    error.error_field = rows.Table.Columns.Contains("field") ? rows["field"].ToString() : "";
                    allObj.error_Lists.Add(error);
                    allObj.status = false;
                    allObj.status_code = 401;
                    allObj.message = "failed";
                }
            }
            return allObj;
        }
    }
}