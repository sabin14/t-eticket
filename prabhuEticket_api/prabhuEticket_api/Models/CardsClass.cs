using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket_api.Models
{
    public class get_cards:returnMain
    {
        public List<cards> data { get; set; }
        public class cards
        {
            public string card_number { get; set; }
            public string card_nfc { get; set; }
            public string balance { get; set; }
            public string type { get; set; }
            public string status { get; set; }
            public string create_by { get; set; }
            public string create_ts { get; set; }
            public string qr_code { get; set; }
            public string initial_balance{get;set;}
            public string credit_limit { get; set; }
        }
    }
    public class register_cards
    {
        public string card_number { get; set; }
        public string card_nfc { get; set; }
        public string balance { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string create_by { get; set; }
        public string qr_code { get; set; }
        public string credit_limit { get; set; }
    }
    public class update_cards
    {
        public string type { get; set; }
        public string qr_code { get; set; }
        public string status { get; set; }
        public string update_by { get; set; }
        public string credit_limit { get; set; }
    }
    public class balance_class
    {
        public string balance { get; set; }
        public string type { get; set; }
        public string user_name { get; set; }
    }
}