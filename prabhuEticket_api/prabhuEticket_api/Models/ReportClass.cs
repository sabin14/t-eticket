using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket_api.Models
{
    public class report_request
    {
        public int fromDate { get; set; }
        public int toDate { get; set; }
    }
    public class report_output: returnMain
    {
        public bool status { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public class Data
        {
            public bool status { get; set; }
            public int totalCashAmount { get; set; }
            public int totalCardAmount { get; set; }
            public int totalNormalPassenger { get; set; }
            public int totalStudentPassenger { get; set; }
            public int totalAmount { get; set; }
        }
    }
}