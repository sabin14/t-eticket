using prabhuEticket.AppCode;
using prabhuEticket.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace prabhuEticket.Repository
{
    public class RoutesRepo : iGetRepo<routes_output>
    {
        public List<routes_output> GetAllData()
        {
            throw new NotImplementedException();
        }

        public List<routes_output> GetAllDataByType(string data)
        {
            throw new NotImplementedException();
        }
        httpConnect connect = new httpConnect();
        JsonObject jObj = new JsonObject();
        public routes_output getRoutes()
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            routes_output output = connect.sendRequest<routes_output>("v1/routes", coll, "", "GET", "application/json");
            return output;
        }
    }
}