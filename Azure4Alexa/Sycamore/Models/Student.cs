using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azure4Alexa.Sycamore.Models
{
    public class Student : StudentBase
    {

        [JsonProperty("Ethnicity")]
        public string Ethnicity { get; set; }

        [JsonProperty("NickName")]
        public string NickName { get; set; }        

        [JsonProperty("Picture")]
        public string Picture { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Cell")]
        public string Cell { get; set; }

        [JsonProperty("AdvisorID")]
        public int AdvisorID { get; set; }

        [JsonProperty("Advisor")]
        public string Advisor { get; set; }

        [JsonProperty("HomeroomTeacherID")]
        public int HomeroomTeacherID { get; set; }

        [JsonProperty("HomeroomTeacher")]
        public string HomeroomTeacher { get; set; }

        [JsonProperty("LockerNum")]
        public string LockerNum { get; set; }

        [JsonProperty("ComboNum")]
        public string ComboNum { get; set; }

        [JsonProperty("StateID")]
        public string StateID { get; set; }

        [JsonProperty("ExtID")]
        public string ExtID { get; set; }        

        [JsonProperty("Location")]
        public string Location { get; set; }
    }

}