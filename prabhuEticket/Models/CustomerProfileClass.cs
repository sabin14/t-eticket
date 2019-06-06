using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prabhuEticket.Models
{
    public class CustomerProfile_request
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Card Number is required")]
        public string cardNumber { get; set; }
    }
    public class CustomerProfile_output
    {
        public bool status { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public CustomerDetail customerDetail { get; set; }

            public class CustomerDetail
            {
                public int id { get; set; }
                public string fullName { get; set; }
                public string firstName { get; set; }
                public object middleName { get; set; }
                public string lastName { get; set; }
                public string customerUniqueId { get; set; }
                public object email { get; set; }
                public string mobile { get; set; }
                public string phone { get; set; }
                public string isSatff { get; set; }
                public string created { get; set; }
                public string imagePath { get; set; }
                public string qrCodeImage { get; set; }
                public CardDetail card_detail { get; set; }
                public class CardDetail
                {
                    public string card_number { get; set; }
                    public string initial_balance { get; set; }
                    public string current_balance { get; set; }
                    public string type { get; set; }
                    public int credit_limit { get; set; }
                }
            }
        }
    }
    public class CustomerProfile_edit_request
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string profilePic { get; set; }
    }
}