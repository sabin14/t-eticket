using prabhuEticket.AppCode;
using prabhuEticket.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
namespace prabhuEticket.Repository
{
    public class CustomerRepo : iGetRepo<CustomerProfile_output>
    {
        globFunction func = new globFunction();
        public CustomerProfile_output getcustomer(DataTable dt = null, string customer_id = "", string card_number = "", string company_id = "")
        {
            if (dt == null)
            {
                dt = new DataTable();
                dt = func.RunSQL("spa_customer_detail @flag='s',@customer_id=" + func.singleQuote(customer_id) + ",@card_number=" + func.singleQuote(card_number) + ",@company_id=" + func.singleQuote(company_id));
            }
            CustomerProfile_output allObj = new CustomerProfile_output();
            allObj.error_Lists = new List<error_list>();
            allObj.data = new List<CustomerProfile_output.CustomerDetail>();
            foreach (DataRow rows in dt.Rows)
            {
                if (rows["code"].ToString() == "0")
                {
                    CustomerProfile_output.CustomerDetail customer = new CustomerProfile_output.CustomerDetail();
                    string first_name = rows["first_name"].ToString();
                    string middle_name = rows["middle_name"].ToString();
                    string last_name = rows["last_name"].ToString();
                    string full_name = first_name + (string.IsNullOrEmpty(middle_name) ? "" : " " + middle_name) + " " + last_name;
                    customer.id = (int)rows["sno"];
                    customer.fullName = full_name;
                    customer.firstName = first_name;
                    customer.middleName = middle_name;
                    customer.lastName = last_name;
                    customer.customerUniqueId = "";// rows["customerUniqueId"].ToString();
                    customer.email = rows["email_address"].ToString();
                    customer.companyID = rows["company_id"].ToString();
                    customer.mobile = rows["mobile_no"].ToString();
                    customer.phone = rows["phone_no"].ToString();
                    customer.isSatff = rows["is_staff"].ToString();
                    customer.created_by = rows["create_by"].ToString();
                    customer.create_ts = rows["create_ts"].ToString();
                    customer.imagePath = rows["pic_path"].ToString();
                    customer.doc_path = rows["doc_path"].ToString();
                    customer.doc_type = rows["doc_type"].ToString();
                    customer.card_detail = new CustomerProfile_output.CustomerDetail.Cards();
                    customer.card_detail.card_number = rows["card_number"].ToString();
                    customer.card_detail.card_nfc = rows["card_nfc"].ToString();
                    customer.card_detail.balance = rows["balance"].ToString();
                    customer.card_detail.type = rows["type"].ToString();
                    customer.card_detail.status = rows["status"].ToString();
                    customer.card_detail.qr_code = rows["qr_code"].ToString();
                    customer.card_detail.initial_balance = rows["initial_balance"].ToString();
                    customer.card_detail.credit_limit = rows["credit_limit"].ToString();
                    allObj.data.Add(customer);
                }
                else
                {
                    error_list error_List = new error_list();
                    error_List.error_code = rows["code"].ToString();
                    error_List.error_message = rows["message"].ToString();
                    allObj.error_Lists.Add(error_List);
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = "failed";
                }
            }
            return allObj;
        }
        public CustomerProfile_output customer_register(customer_register item, string agent_code="")
        {
            string sql = "spa_customer_detail @flag='i',@first_name=" + func.singleQuote(item.first_name) +
            ", @middle_name = " + func.singleQuote(item.middle_name) +
            ", @last_name = " + func.singleQuote(item.last_name) +
            ", @email_address = " + func.singleQuote(item.email_address) +
            ", @mobile_no = " + func.singleQuote(item.mobile_no) +
            ", @phone_no = " + func.singleQuote(item.phone_no) +
            ", @is_staff = " + func.singleQuote(item.is_staff) +
            ", @card_number = " + func.singleQuote(item.card_number) +
            ", @company_id = " + func.singleQuote(agent_code) +
            ", @pic_path = " + func.singleQuote(item.pic_path) +
            ", @doc_type = " + func.singleQuote(item.doc_type) +
            ", @doc_path = " + func.singleQuote(item.doc_path) +
            ", @user_name=" + func.singleQuote(item.create_by) +
             ", @balance=" + func.singleQuote(item.balance) +
              ", @qr_code=" + func.singleQuote(item.qr_code) +
               ", @credit_limit=" + func.singleQuote(item.credit_limit);
            DataTable dt = func.RunSQL(sql);
            CustomerProfile_output output = getcustomer(dt);
            return output;
        }
        public CustomerProfile_output customer_update(CustomerProfile_edit_request item, string agent_code="", string customer_id="")
        {
            string sql = "spa_customer_detail @flag='u',@first_name=" + func.singleQuote(item.first_name) + ",@sno=" + func.singleQuote(customer_id) +
            ", @middle_name = " + func.singleQuote(item.middle_name) +
            ", @last_name = " + func.singleQuote(item.last_name) +
            ", @email_address = " + func.singleQuote(item.email_address) +
            ", @mobile_no = " + func.singleQuote(item.mobile_no) +
            ", @phone_no = " + func.singleQuote(item.phone_no) +
            ", @company_id = " + func.singleQuote(agent_code) +
            ", @pic_path = " + func.singleQuote(item.pic_path) +
            ", @doc_type = " + func.singleQuote(item.doc_type) +
            ", @doc_path = " + func.singleQuote(item.doc_path) +
            ", @user_name=" + func.singleQuote(item.update_by) +
            ", @qr_code=" + func.singleQuote(item.qr_code)+
            ", @type="+func.singleQuote(item.customer_type);

            DataTable dt = func.RunSQL(sql);
            CustomerProfile_output output = getcustomer(dt);
            return output;
        }
        public returnMain customer_delete(customer_delete item, string agent_code="", string customer_id="")
        {
            string sql = "spa_customer_detail @flag='u',@is_active='n',@sno=" + func.singleQuote(customer_id) + ",@card_number=" + func.singleQuote(item.card_number) +
            ", @user_name=" + func.singleQuote(item.user_name);
            DataTable dt = func.RunSQL(sql);
            returnMain allObj = new returnMain();
            allObj.error_Lists = new List<error_list>();
            foreach (DataRow rows in dt.Rows)
            {
                if (rows["code"].ToString() == "0")
                {
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = rows["message"].ToString();
                }
                else
                {
                    error_list error_List = new error_list();
                    error_List.error_code = rows["code"].ToString();
                    error_List.error_message = rows["message"].ToString();
                    allObj.error_Lists.Add(error_List);
                    allObj.status_code = 401;
                    allObj.status = false;
                    allObj.message = rows["message"].ToString();
                }
            }
            return allObj;
        }
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
        public CustomerProfile_output getCustomerProfile(CustomerProfile_request request, string token)
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            coll.Add("access-token", token);
            string data = "cardNumber=" + request.card_number;
            CustomerProfile_output output = connect.sendRequest<CustomerProfile_output>("v1/company/customer/profile", coll, data, "GET", "application/json");
            return output;
        }
        public CustomerProfile_output editCustomerProfile(CustomerProfile_edit_request request, string id, string token)
        {
            WebHeaderCollection coll = new WebHeaderCollection();
            coll.Add("api-key", ConfigurationManager.AppSettings["api_key"] == null ? "L7cWaOsSuQ0gfE1BvHEzgTL2Z-qcjfbxBdXoIu3Dg9A" : ConfigurationManager.AppSettings["api_key"].ToString());
            coll.Add("access-token", token);
            string data = jObj.Serialize<CustomerProfile_edit_request>(request);
            CustomerProfile_output output = connect.sendRequest<CustomerProfile_output>("v1/company/customer/profile/" + id + "/edit", coll, data, "POST", "application/json");
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