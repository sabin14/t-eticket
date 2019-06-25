using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using prabhuEticket_api.Models;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace prabhuEticket_api.AppCode
{
    public class CheckAPIKey
    {
        public authenticate_output checkAuthentication(HttpRequestHeaders headers, HttpRequestMessage message)
        {
            globFunction func = new globFunction();
            authenticate_output allObj = new authenticate_output();
            allObj.error_Lists = new List<error_list>();
            string credentials = "";
            try
            {
                if (headers.Contains("api-key") && headers.Contains("access-token"))
                {
                    allObj = new authenticate_output();
                    string api_key, access_token;
                    try
                    {
                        api_key = headers.GetValues("api-key").First();
                        access_token = headers.GetValues("access-token").First();
                    }
                    catch
                    {
                        api_key = "";
                        access_token = "";
                    }
                    if (String.IsNullOrEmpty(api_key) || string.IsNullOrEmpty(access_token))
                    {
                        //errors = new error_list();
                        //errors.error_message = "There was an error when processing your request.";
                        //errors.error_code = "E5999";
                        //allObj.error_list.Add(errors);
                        //allObj.process_result = false;
                        return allObj;
                    }
                    else
                    {
                        var api_key_value = AuthenticationHeaderValue.Parse(api_key);
                        var access_token_value = AuthenticationHeaderValue.Parse(access_token);
                        api_key = (api_key_value.Scheme != null ? api_key_value.Scheme : "") +
                            (api_key_value.Parameter != null ? api_key_value.Parameter : "");
                        access_token = (access_token_value.Scheme != null ? access_token_value.Scheme : "") +
                            (access_token_value.Parameter != null ? access_token_value.Parameter : "");
                        GetIPAddress getIp = new GetIPAddress();
                        var ipAddr = getIp.getIPAdd(message);
                        var ipAdd = IPAddress.Parse(ipAddr);
                        string location = globFunction.GetUserCountryByIp(ipAdd.ToString());
                        DataTable dt = func.RunSQL("spa_login @flag='c',@access_token=" + func.singleQuote(access_token) + ",@api_key=" + func.singleQuote(api_key) +
                            ",@location_trace=" + func.singleQuote(location) + ",@ip_address=" + func.singleQuote(ipAdd.ToString()));
                        allObj.status = true;
                        allObj.data = new authenticate_output.Data();
                        allObj.data.userInfo = new authenticate_output.Data.UserInfo();
                        allObj.data.userInfo.settings = new authenticate_output.Data.UserInfo.Settings();
                        foreach (DataRow rows in dt.Rows)
                        {
                            allObj = fetchLoginRows(rows);
                        }
                        return allObj;
                    }
                }
                else
                {
                    allObj.status = false;
                    allObj.error_Lists = new List<error_list>();
                    allObj.error_Lists.Add(new error_list() { error_code = "401", error_message = "Not Authorized." });
                    allObj.message = "failed";
                    allObj.status_code = 401;
                    return allObj;
                }
            }
            catch
            {
                //errors = new error_list();
                //errors.error_message = "There was an error when processing your request.";
                //errors.error_code = "E5999";
                //allObj.error_list.Add(errors);
                //allObj.process_result = false;
                allObj.status = false;
                allObj.error_Lists = new List<error_list>();
                allObj.error_Lists.Add(new error_list() { error_code = "401", error_message = "Not Authorized." });
                allObj.message = "failed";
                allObj.status_code = 401;
                return allObj;
            }
        }
        public authenticate_output fetchLoginRows(DataRow rows)
        {
            globFunction func = new globFunction();
            authenticate_output allObj = new authenticate_output();
            allObj.error_Lists = new List<error_list>();
            allObj.status = true;
            allObj.data = new authenticate_output.Data();
            allObj.data.userInfo = new authenticate_output.Data.UserInfo();
            allObj.data.userInfo.settings = new authenticate_output.Data.UserInfo.Settings();
            error_list error = new error_list();
            string code = rows["code"].ToString();
            if (code != "0" || allObj.status == false)
            {
                error.error_code = code;
                error.error_message = rows["message"].ToString();
                error.error_field = rows.Table.Columns.Contains("field") ? rows["field"].ToString() : "";
                allObj.error_Lists.Add(error);
                allObj.status = false;
                allObj.status_code = 401;
                allObj.message = "failed";
            }
            else
            {
                allObj.status_code = 200;
                allObj.message = "success";
                allObj.data.message = "success";
                allObj.data.role = rows["role_id"].ToString();
                allObj.data.path = rows["path"].ToString();
                allObj.data.token = rows["access_token"].ToString();
                allObj.data.userInfo.address = rows["address"].ToString();
                allObj.data.userInfo.company = "";// rows[""].ToString();
                allObj.data.userInfo.contact = rows["contact_no"].ToString();
                allObj.data.userInfo.image = rows["img_path"].ToString();
                allObj.data.userInfo.name = rows["full_name"].ToString();
                allObj.data.userInfo.qrCodeImage = rows["qrCodeImage"].ToString();
                allObj.data.userInfo.username = rows["user_name"].ToString();
                allObj.data.userInfo.settings.vehicle_minimum_fare_amount = rows["vehicle_minimum_fare"].ToString();
                allObj.data.userInfo.settings.vehicle_oldage_discount_rate = rows["elderly_discount_rate"].ToString();
                allObj.data.userInfo.settings.vehicle_student_discount_rate = rows["student_discount_rate"].ToString();
                allObj.data.userInfo.settings.handicapped_discoun_rate = rows["handicapped_discount_rate"].ToString();
            }
            return allObj;
        }
    }
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        CheckAPIKey CheckAPIKey = new CheckAPIKey();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            authenticate_output auth = CheckAPIKey.checkAuthentication(actionContext.Request.Headers, actionContext.Request);
            if (auth.status_code != 0&&auth.status_code!=200)
            {
                actionContext.Response = actionContext.Request.CreateResponse<authenticate_output>(
                HttpStatusCode.OK, auth);
            }
        }
    }
}