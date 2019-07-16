using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prabhuEticket.Models
{
    public class CustomerProfile_request
    {
        //[Required(AllowEmptyStrings =false,ErrorMessage ="Card Number is required")]
        public string card_number { get; set; }
    }
    public class CustomerProfile_output : returnMain
    {
        public List<CustomerDetail> data { get; set; }
        public class CustomerDetail
        {
            public int id { get; set; }
            public string companyID { get; set; }
            public string fullName { get; set; }
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public string customerUniqueId { get; set; }
            public string email { get; set; }
            public string mobile { get; set; }
            public string phone { get; set; }
            public string isSatff { get; set; }
            public string created_by { get; set; }
            public string create_ts { get; set; }
            public string imagePath { get; set; }
            public string doc_type { get; set; }
            public string doc_path { get; set; }
            public Cards card_detail { get; set; }
            public class Cards
            {
                public string card_number { get; set; }
                public string card_nfc { get; set; }
                public string balance { get; set; }
                public string type { get; set; }
                public string status { get; set; }
                public string qr_code { get; set; }
                public string initial_balance { get; set; }
                public string credit_limit { get; set; }
            }
        }

    }
    public class CustomerProfile_edit_request
    {
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email_address { get; set; }
        public string companyID { get; set; }
        public string mobile_no { get; set; }
        public string phone_no { get; set; }
        public string pic_path { get; set; }
        public string doc_type { get; set; }
        public string doc_path { get; set; }
        public string update_by { get; set; }
        public string qr_code { get; set; }
        public string customer_type { get; set; }
    }
    public class internal_CustomerProfile_edit_request
    {
        public string customer_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email_address { get; set; }
        public string companyID { get; set; }
        public string mobile_no { get; set; }
        public string phone_no { get; set; }
        public string pic_path { get; set; }
        public string doc_type { get; set; }
        public string doc_path { get; set; }
        public string update_by { get; set; }
        public string qr_code { get; set; }
        public string customer_type { get; set; }
    }
    public class customer_register
    {
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email_address { get; set; }
        public string mobile_no { get; set; }
        public string phone_no { get; set; }
        public bool is_staff { get; set; }
        public string companyID { get; set; }
        public string card_number { get; set; }
        public string pic_path { get; set; }
        public string doc_type { get; set; }
        public string doc_path { get; set; }
        public string create_by { get; set; }
        public string qr_code { get; set; }
        public string customer_type { get; set; }
        public string balance { get; set; }
        public string credit_limit { get; set; }
    }
    public class customer_delete
    {
        public string card_number { get; set; }
        public string user_name { get; set; }
    }
}