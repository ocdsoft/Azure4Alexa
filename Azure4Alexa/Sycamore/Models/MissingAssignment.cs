using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azure4Alexa.Sycamore.Models
{
    public class MissingAssignment
    {

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("ClassID")]
        public int ClassID { get; set; }

        [JsonProperty("ClassName")]
        public string ClassName { get; set; }

        [JsonProperty("AssignmentID")]
        public int AssignmentID { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("DueDate")]
        public string DueDate { get; set; }
    }

}