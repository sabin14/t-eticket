using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prabhuEticket.Models;
using prabhuEticket.AppCode;
using System.Net;
using System.Configuration;

namespace prabhuEticket.Repository
{
    public class AuthenticationRepo : iGetRepo<authenticate_output>
    {
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