using System.ComponentModel.DataAnnotations;

namespace prabhuEticket.Models
{


    public class authenticate_output
    {
        public class Data
        {
            public string message { get; set; }
            public string token { get; set; }
            public string role { get; set; }
            public UserInfo userInfo { get; set; }
            public class UserInfo
            {
                public class Settings
                {
                    public string vehicle_minimum_fare_amount { get; set; }
                    public string vehicle_student_discount_rate { get; set; }
                    public string vehicle_oldage_discount_rate { get; set; }
                }

                public string name { get; set; }
                public string username { get; set; }
                public object contact { get; set; }
                public string company { get; set; }
                public object address { get; set; }
                public string image { get; set; }
                public object qrCodeImage { get; set; }
                public Settings settings { get; set; }
            }
        }
        public bool status { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
    public class authenticate_request
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Name is required.")]
        public string username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        public string password { get; set; }
    }
}