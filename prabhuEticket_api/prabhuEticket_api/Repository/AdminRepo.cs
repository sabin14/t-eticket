using prabhuEticket_api.AppCode;
using prabhuEticket_api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace prabhuEticket_api.Repository
{
    public class AdminRepo
    {
        globFunction func = new globFunction();
        public roles GetRoles()
        {
            roles allObj = new roles();
            allObj.roles_list = new List<roles.role>();
            allObj.error_Lists = new List<error_list>();
            DataTable dt = func.RunSQL("spa_admin_table @flag='sr'");
            foreach (DataRow row in dt.Rows)
            {
                if (row["code"].ToString() == "0")
                {
                    roles.role role = new roles.role();
                    role.role_id = row["role_id"].ToString();
                    role.role_name = row["role_name"].ToString();
                    allObj.roles_list.Add(role);
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = row["message"].ToString();
                }
                else
                {
                    error_list error_List = new error_list();
                    error_List.error_code = row["code"].ToString();
                    error_List.error_message = row["message"].ToString();
                    allObj.error_Lists.Add(error_List);
                    allObj.status = false;
                    allObj.status_code = 401;
                    allObj.message = "failed";
                }
            }
            return allObj;
        }
    }
}