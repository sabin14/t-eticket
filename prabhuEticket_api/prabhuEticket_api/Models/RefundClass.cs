using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket_api.Models
{
    public class fullRefund_request
    {
        public string cardNumber { get; set; }
    }
    

    public class fullRefund_output: returnMain
    {
        public bool status { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public class Data
        {
            public bool status { get; set; }
            public string customer { get; set; }
            public string transactionId { get; set; }
            public int availableBalance { get; set; }
        }
    }
}