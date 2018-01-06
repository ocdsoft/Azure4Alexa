using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azure4Alexa.Sycamore.Models
{
    public class Me
    {

        [JsonProperty("UserID")]
        public int UserID { get; set; }

        [JsonProperty("SchoolID")]
        public int SchoolID { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("FamilyID")]
        public int FamilyID { get; set; }

        [JsonProperty("StudentID")]
        public int StudentID { get; set; }

        [JsonProperty("Level")]
        public string Level { get; set; }

        [JsonProperty("SuperUser")]
        public int SuperUser { get; set; }

        [JsonProperty("Active")]
        public int Active { get; set; }

        [JsonProperty("Current")]
        public int Current { get; set; }

        [JsonProperty("Scopes")]
        public string Scopes { get; set; }
    }

}