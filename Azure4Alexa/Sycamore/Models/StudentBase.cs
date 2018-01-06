using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azure4Alexa.Sycamore.Models
{
    public class StudentBase
    {

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Grade")]
        public string Grade { get; set; }

        [JsonProperty("DOB")]
        public string DOB { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }
    }

}