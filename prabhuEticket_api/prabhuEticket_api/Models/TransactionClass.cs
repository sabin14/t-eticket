using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket_api.Models
{
    public class transaction_request
    {
        public string from { get; set; }
        public string to { get; set; }
        public int quantity { get; set; }
        public int location { get; set; }
        public float amount { get; set; }
        public string cardNumber { get; set; }
    }
    public class transaction_output: returnMain
    {
        public bool status { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public class Data
        {
            public bool status { get; set; }
            public int totalCashAmount { get; set; }
            public double totalCardAmount { get; set; }
            public int totalNormalPassenger { get; set; }
            public int totalStudentPassenger { get; set; }
            public double totalAmount { get; set; }
        }

    }
}