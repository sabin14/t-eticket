using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket_api.Models
{
    public class company_group
    {
        public string group_id { get; set; }
        public string group_name { get; set; }
    }
    public class company_Groups : returnMain
    {
        public List<company_group> groups { get; set; }
        
    }
    public class company_details_output : returnMain
    {
        public List<company_detail> data { get; set; }
        public class company_detail
        {
            public string agent_code { get; set; }
            public string agent_name { get; set; }
            public string country { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public string company_code { get; set; }
            public company_group group { get; set; }
            public string user_name { get; set; }
            public string doc_path { get; set; }
            public string img_path { get; set; }
            public string is_sub_agent { get; set; }
            public string primary_agent_code { get; set; }
            public string vehicle_minimum_fare { get; set; }
            public string student_discount_rate { get; set; }
            public string elderly_discount_rate { get; set; }
            public string handicapped_discount_rate { get; set; }
            public string create_by { get; set; }
            public string create_ts { get; set; }
            public string update_by { get; set; }
            public string update_ts { get; set; }
        }
    }
    public class updateCompany_user_name
    {
        public string user_name { get; set; }
    }
    public class updateCompany_password
    {
        public string password { get; set; }
    }
    public class deleteCompany
    {
        public string user_name { get; set; }
    }
    public class register_company {
        public string agent_name { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string company_code { get; set; }
        public string group_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string doc_path { get; set; }
        public string create_by { get; set; }
        public string vehicle_minimum_fare { get; set; }
        public string student_discount_rate { get; set; }
        public string elderly_discount_rate { get; set; }
        public string handicapped_discount_rate { get; set; }
        public string api_key { get; set; }
        public string img_path { get; set; }
    }
    public class update_company
    {
        public string agent_name { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string company_code { get; set; }
        public string group_id { get; set; }
        public string doc_path { get; set; }
        public string update_by { get; set; }
        public string vehicle_minimum_fare { get; set; }
        public string student_discount_rate { get; set; }
        public string elderly_discount_rate { get; set; }
        public string handicapped_discount_rate { get; set; }
        public string api_key { get; set; }
        public string img_path { get; set; }
    }
    public class company_contacts:returnMain
    {
        public List<company_contact_output> data { get; set; }
        public class company_contact_output
        {
            public string contact_id { get; set; }
            public string contact_name { get; set; }
            public string email_address { get; set; }
            public string designation { get; set; }
            public string contact_no { get; set; }
            public string mobile_no { get; set; }
            public string agent_code { get; set; }
            public string create_by { get; set; }
            public string create_ts { get; set; }
            public string update_by { get; set; }
            public string update_ts { get; set; }
        }
    }
    public class register_company_contact {
        public string contact_name { get; set; }
        public string email_address { get; set; }
        public string designation { get; set; }
        public string contact_no { get; set; }
        public string mobile_no { get; set; }
        public string create_by { get; set; }
    }
    public class update_company_contact
    {
        public string contact_name { get; set; }
        public string email_address { get; set; }
        public string designation { get; set; }
        public string contact_no { get; set; }
        public string mobile_no { get; set; }
        public string update_by { get; set; }
    }
}