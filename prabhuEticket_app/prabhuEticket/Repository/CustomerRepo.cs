using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using prabhuEticket.AppCode;
using prabhuEticket.Models;
namespace prabhuEticket.Repository
{
    public class CustomerRepo : iGetRepo<CustomerProfile_output>
    {
        public List<CustomerProfile_output> GetAllData()
        {
            throw new NotImplementedException();
        }

        public List<CustomerProfile_output> GetAllDataByType(string data)
        {
            throw new NotImplementedException();
        }
        httpConnect connect = new httpConnect();
        JsonObject jObj = new JsonObject();
        public CustomerProfile_output getCustomerProfile(CustomerProfile_request request,string token)
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            coll.Add("access-token", token);
            string data = "cardNumber="+request.cardNumber;
            CustomerProfile_output output = connect.sendRequest<CustomerProfile_output>("v1/company/customer/profile", coll, data, "GET", "application/json");
            return output;
        }
        public CustomerProfile_output editCustomerProfile(CustomerProfile_edit_request request,string id,string token)
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            coll.Add("access-token", token);
            string data = jObj.Serialize<CustomerProfile_edit_request>(request);
            CustomerProfile_output output = connect.sendRequest<CustomerProfile_output>("v1/company/customer/profile/"+id+"/edit", coll, data, "POST", "application/json");
            return output;
        }
        public fullRefund_output getFullRefund(fullRefund_request request, string token)
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            coll.Add("access-token", token);
            string data = jObj.Serialize<fullRefund_request>(request);
            fullRefund_output output = connect.sendRequest<fullRefund_output>("v1/full-refund", coll, data, "POST", "application/json");
            return output;
        }
        public transaction_output sendTransaction(transaction_request request, string token)
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            coll.Add("access-token", token);
            string data = jObj.Serialize<transaction_request>(request);
            transaction_output output = connect.sendRequest<transaction_output>("v1/transaction", coll, data, "POST", "application/json");
            return output;
        }
    }
}