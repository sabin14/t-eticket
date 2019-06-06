using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket.Models
{
    public class routes_output
    {
        public bool status { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public class Data
        {
            public List<StopsArray> stopsArray { get; set; }
            public VehicleConfigs vehicleConfigs { get; set; }
            public class StopsArray
            {
                public string name { get; set; }
                public int cost { get; set; }
                public int order { get; set; }
            }

            public class VehicleConfigs
            {
                public string vehicle_minimum_fare_amount { get; set; }
                public string vehicle_student_discount_rate { get; set; }
                public string vehicle_oldage_discount_rate { get; set; }
            }
        }
    }
}