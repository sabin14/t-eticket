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
    public class ReportRepo : iGetRepo<routes_output>
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
        public report_output getTransactionReport(report_request request, string token)
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            coll.Add("access-token", token);
            string data = jObj.Serialize<report_request>(request);
            report_output output = connect.sendRequest<report_output>("v1/transaction/report", coll, data, "POST", "application/json");
            return output;
        }
    }
}