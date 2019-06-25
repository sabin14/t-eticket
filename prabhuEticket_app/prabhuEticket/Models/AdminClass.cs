using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket.Models
{
    public class AdminClass
    {
    }
    public class roles:returnMain
    {
        public List<role> roles_list { get; set; }   
        public class role
        {
            public string role_id { get; set; }
            public string role_name { get; set; }
        }
    }
}