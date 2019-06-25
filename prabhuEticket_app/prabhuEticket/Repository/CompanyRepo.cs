using prabhuEticket.AppCode;
using prabhuEticket.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace prabhuEticket.Repository
{
    public class CompanyRepo
    {
        globFunction func = new globFunction();
        public company_Groups GetGroups()
        {
            DataTable dt = func.RunSQL("spa_company_detail @flag='g'");
            company_Groups allObj = new company_Groups();
            allObj.error_Lists = new List<error_list>();
            allObj.groups = new List<company_group>();
            foreach (DataRow rows in dt.Rows)
            {
                if (rows["code"].ToString() == "0")
                {
                    company_group company_Group = new company_group();
                    company_Group.group_id = rows["group_id"].ToString();
                    company_Group.group_name = rows["group_name"].ToString();
                    allObj.groups.Add(company_Group);
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
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = rows["message"].ToString();
                }
            }
            return allObj;
        }
        public company_details_output company_Details_Output(DataTable dt=null, string agent_code = "", string primary_agent_code = "")
        {
            if (dt == null)
            {
                dt = new DataTable();

                dt = func.RunSQL("spa_company_detail @flag='" + (string.IsNullOrEmpty(primary_agent_code) ? "s" : "ss") + "'," +
                    "@agent_code=" + func.singleQuote(agent_code) + ",@primary_agent_code=" + func.singleQuote(primary_agent_code));
            }
            company_details_output allObj = new company_details_output();
            allObj.error_Lists = new List<error_list>();
            allObj.data = new List<company_details_output.company_detail>();

            foreach (DataRow rows in dt.Rows)
            {
                if (rows["code"].ToString() == "0")
                {
                    company_details_output.company_detail company = new company_details_output.company_detail();
                    company.group = new company_group();
                    company.group.group_id = rows["group_id"].ToString();
                    company.group.group_name = rows["group_name"].ToString();
                    company.agent_code = rows["agent_code"].ToString();
                    company.agent_name = rows["agent_name"].ToString();
                    company.country = rows["country"].ToString();
                    company.state = rows["state"].ToString();
                    company.city = rows["city"].ToString();
                    company.address = rows["address"].ToString();
                    company.phone = rows["phone"].ToString();
                    company.website = rows["website"].ToString();
                    company.company_code = rows["company_code"].ToString();
                    company.user_name = rows["user_name"].ToString();
                    company.doc_path = rows["doc_path"].ToString();
                    company.img_path = rows["img_path"].ToString();
                    company.is_sub_agent = rows["is_sub_agent"].ToString();
                    company.primary_agent_code = rows["primary_agent_code"].ToString();
                    company.vehicle_minimum_fare = rows["vehicle_minimum_fare"].ToString();
                    company.student_discount_rate = rows["student_discount_rate"].ToString();
                    company.elderly_discount_rate = rows["elderly_discount_rate"].ToString();
                    company.handicapped_discount_rate = rows["handicapped_discount_rate"].ToString();
                    company.create_by = rows["create_by"].ToString();
                    company.create_ts = rows["create_ts"].ToString();
                    company.update_by = rows["update_by"].ToString();
                    company.update_ts = rows["update_ts"].ToString();
                    allObj.data.Add(company);
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
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = "failed";
                }
            }
            return allObj;
        }
        public company_details_output registerCompanyOrSub(register_company item,string primary_agent_code="")
        {
            string sql = "[dbo].[spa_company_detail] @flag = 'i',@is_sub_agent =" +(string.IsNullOrEmpty(primary_agent_code)?"":"y")+","+
                "@phone = " + func.singleQuote(item.phone) + ",@country = " + func.singleQuote(item.country) + ",@state = " + func.singleQuote(item.state) + ",@company_code = " + func.singleQuote(item.company_code) + "," +
                "@user_name = " + func.singleQuote(item.user_name) + ",@city = " + func.singleQuote(item.city) + ",@website = " + func.singleQuote(item.website) + ",@create_by = " + func.singleQuote(item.create_by) + "," +
                "@agent_name = " + func.singleQuote(item.agent_name) + ",@primary_agent_code = " + (string.IsNullOrEmpty(primary_agent_code) ? "" : primary_agent_code) + "," +
                "@api_key = " + func.singleQuote(item.api_key) + ",@address = " + func.singleQuote(item.address) + ",@doc_path = " + func.singleQuote(item.doc_path) + "," +
                "@password = " + func.singleQuote(item.password) + ",@group_id = " + func.singleQuote(item.group_id) + "," +
                "@vehicle_minimum_fare = " + func.singleQuote(item.vehicle_minimum_fare) + ",@student_discount_rate =" + func.singleQuote(item.student_discount_rate) + "," +
                "@elderly_discount_rate =" + func.singleQuote(item.elderly_discount_rate) + ",@handicapped_discount_rate = " + func.singleQuote(item.handicapped_discount_rate) + "," +
                "@is_active = 'y',@img_path = "+func.singleQuote(item.img_path);
            DataTable dt = func.RunSQL(sql);
            company_details_output _Output = company_Details_Output(dt);
            return _Output;
        }
        public company_details_output updateCompanyOrSub(update_company item,string agent_code, string primary_agent_code = "")
        {
            string sql = "[dbo].[spa_company_detail] @flag = 'u',@agent_code="+func.singleQuote(agent_code)+",@is_sub_agent =" + (string.IsNullOrEmpty(primary_agent_code) ? "" : "y") + "," +
                "@phone = " + func.singleQuote(item.phone) + ",@country = " + func.singleQuote(item.country) + ",@state = " + func.singleQuote(item.state) + ",@company_code = " + func.singleQuote(item.company_code) + "," +
                "@city = " + func.singleQuote(item.city) + ",@website = " + func.singleQuote(item.website) + ",@create_by = " + func.singleQuote(item.update_by) + "," +
                "@agent_name = " + func.singleQuote(item.agent_name) + ",@primary_agent_code = " + (string.IsNullOrEmpty(primary_agent_code) ? "" : primary_agent_code) + "," +
                "@api_key = " + func.singleQuote(item.api_key) + ",@address = " + func.singleQuote(item.address) +
                ",@doc_path = " + func.singleQuote(item.doc_path) + "," +"@group_id = " + func.singleQuote(item.group_id) + "," +
                "@vehicle_minimum_fare = " + func.singleQuote(item.vehicle_minimum_fare) + ",@student_discount_rate =" + func.singleQuote(item.student_discount_rate) + "," +
                "@elderly_discount_rate =" + func.singleQuote(item.elderly_discount_rate) + ",@handicapped_discount_rate = " + func.singleQuote(item.handicapped_discount_rate) + "," +
                "@is_active = 'y',@img_path = " + func.singleQuote(item.img_path);
            DataTable dt = func.RunSQL(sql);
            company_details_output _Output = company_Details_Output(dt);
            return _Output;
        }
        public returnMain updateUsername(updateCompany_user_name item,string agent_code,string primary_agent_code="")
        {
            returnMain allObj = new returnMain();
            allObj.error_Lists = new List<error_list>();
            DataTable dt = func.RunSQL("spa_company_detail @flag='uun',@agent_code="+func.singleQuote(agent_code)+
                ",@user_name=" + func.singleQuote(item.user_name) +
                ",@primary_agent_code=" +func.singleQuote(primary_agent_code));
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
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = rows["message"].ToString();
                }
            }
            return allObj;
        }
        public returnMain updatePassword(updateCompany_password item, string agent_code, string primary_agent_code = "")
        {
            returnMain allObj = new returnMain();
            allObj.error_Lists = new List<error_list>();
            DataTable dt = func.RunSQL("spa_company_detail @flag='uup',@agent_code=" + func.singleQuote(agent_code) +
                ",@password="+func.singleQuote(item.password)+
                ",@primary_agent_code=" + func.singleQuote(primary_agent_code));
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
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = rows["message"].ToString();
                }
            }
            return allObj;
        }
        public returnMain deleteCompany(string user_name,string agent_code, string primary_agent_code = "")
        {
            returnMain allObj = new returnMain();
            allObj.error_Lists = new List<error_list>();
            DataTable dt = func.RunSQL("spa_company_detail @flag='u',@agent_code=" + func.singleQuote(agent_code) +
                ",@is_active='n'"+
                ",@primary_agent_code=" + func.singleQuote(primary_agent_code)+",@user_name="+func.singleQuote(user_name));
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
                    allObj.status = true;
                    allObj.message = rows["message"].ToString();
                }
            }
            return allObj;
        }
        public company_contacts GetCompany_Contacts(DataTable dt = null, string agent_code="",string contact_id="")
        {
            if (dt == null)
            {
                dt = new DataTable();
                dt = func.RunSQL("spa_company_detail @flag='ccs',@agent_code=" + func.singleQuote(agent_code) +
                ",@contact_id=" + func.singleQuote(contact_id));
            }
            company_contacts allObj = new company_contacts();
            allObj.error_Lists = new List<error_list>();
            allObj.data = new List<company_contacts.company_contact_output>();
            foreach (DataRow rows in dt.Rows)
            {
                if (rows["code"].ToString() == "0")
                {
                    company_contacts.company_contact_output item = new company_contacts.company_contact_output();
                    item.contact_id = rows["sno"].ToString();
                    item.contact_name = rows["contact_name"].ToString();
                    item.email_address = rows["email_address"].ToString();
                    item.designation = rows["designation"].ToString();
                    item.contact_no = rows["contact_no"].ToString();
                    item.mobile_no = rows["mobile_no"].ToString();
                    item.agent_code = rows["company_id"].ToString();
                    item.create_by = rows["create_by"].ToString();
                    item.create_ts = rows["create_ts"].ToString();
                    item.update_by = rows["update_by"].ToString();
                    item.update_ts = rows["update_ts"].ToString();
                    allObj.data.Add(item);
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
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = rows["message"].ToString();
                }
            }
            return allObj;
        }
        public company_contacts registerCompany_Contacts(register_company_contact item, string agent_code)
        {
            company_contacts allObj = new company_contacts();
            DataTable dt = func.RunSQL("spa_company_detail @flag='ccr',@agent_name=" + func.singleQuote(item.contact_name) +
            ",@email_address=" + func.singleQuote(item.email_address) +
            ",@designation=" + func.singleQuote(item.designation) +
            ",@phone=" + func.singleQuote(item.contact_no) +
            ",@mobile_no=" + func.singleQuote(item.mobile_no) +
            ",@agent_code=" + func.singleQuote(agent_code) +
            ",@user_name=" + func.singleQuote(item.create_by));
            allObj = GetCompany_Contacts(dt);
            return allObj;
        }
        public company_contacts updateCompany_Contacts(update_company_contact item, string agent_code,string contact_id)
        {
            company_contacts allObj = new company_contacts();
            DataTable dt = func.RunSQL("spa_company_detail @flag='ccu',@agent_name=" + func.singleQuote(item.contact_name) +
            ",@email_address=" + func.singleQuote(item.email_address) +
            ",@designation=" + func.singleQuote(item.designation) +
            ",@phone=" + func.singleQuote(item.contact_no) +
            ",@mobile_no=" + func.singleQuote(item.mobile_no) +
            ",@agent_code=" + func.singleQuote(agent_code) +
            ",@user_name=" + func.singleQuote(item.update_by)+ ",@contact_id="+func.singleQuote(contact_id));
            allObj = GetCompany_Contacts(dt);
            return allObj;
        }
        public returnMain deleteCompanyContact(string user_name, string agent_code, string contact_id)
        {
            returnMain allObj = new returnMain();
            allObj.error_Lists = new List<error_list>();
            DataTable dt = func.RunSQL("spa_company_detail @flag='ccd',@agent_code=" + func.singleQuote(agent_code) +
                ",@contact_id=" + func.singleQuote(contact_id) + ",@user_name=" + func.singleQuote(user_name));
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
                    allObj.status = true;
                    allObj.message = rows["message"].ToString();
                }
            }
            return allObj;
        }
    }
}