using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prabhuEticket_api.Models
{
    public class IpInfo
    {

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }
    }
    public class error_list
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string error_code { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string error_message { get; set; }
        public string error_field { get; set; }
    }
    public class returnMain
    {
        public List<error_list> error_Lists { get; set; }
    }
}