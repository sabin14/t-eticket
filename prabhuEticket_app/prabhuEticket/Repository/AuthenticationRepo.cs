using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prabhuEticket.Models;
using prabhuEticket.AppCode;
using System.Net;
using System.Configuration;
using System.Net.Http;
using System.Data;

namespace prabhuEticket.Repository
{
    public class AuthenticationRepo : iGetRepo<authenticate_output>
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
                            ",@location_trace=" + func.singleQuote(location) + ",@ip_address=" + func.singleQuote(ipAdd.ToString()) + ",@user_name=" + func.singleQuote(request.username) +
                            ",@password=" + func.singleQuote(request.password));
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
        public List<authenticate_output> GetAllData()
        {
            throw new NotImplementedException();
        }

        public List<authenticate_output> GetAllDataByType(string data)
        {
            throw new NotImplementedException();
        }
        httpConnect connect = new httpConnect();
        JsonObject jObj = new JsonObject();
        /// <summary>
        /// generate token(used for login)
        /// </summary>
        /// <param name="request">object containing username and password</param>
        /// <returns></returns>
        public authenticate_output generateToken(authenticate_request request)
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            string data = jObj.Serialize<authenticate_request>(request);
            authenticate_output output = connect.sendRequest<authenticate_output>("v1/authenticate", coll, data, "POST", "application/json");
            return output;
        }

    }
}