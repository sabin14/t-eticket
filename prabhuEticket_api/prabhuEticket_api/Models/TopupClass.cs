using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket_api.Models
{
    public class topup_request
    {
        public int location { get; set; }
        public float amount { get; set; }
        public string cardNumber { get; set; }
    }
    public class topup_output: returnMain
    {
        public bool status { get; set; }
        public string customer { get; set; }
        public string transactionId { get; set; }
        public int availableBalance { get; set; }
        public string message { get; set; }
    }
}