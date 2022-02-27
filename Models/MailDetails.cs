using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCNX.Models
{
    public class MailDetails
    {
       public string SRNO { get; set; }
       public string DATE { get; set; }
       public string SUBJECT { get; set; }
       public string MESSAGE { get; set; }
       public string TOPIC { get; set; }
       public string IMG { get; set; }
       
    }
    public class reCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("challenge_ts")]
        public string ValidatedDateTime { get; set; }

        [JsonProperty("hostname")]
        public string HostName { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}