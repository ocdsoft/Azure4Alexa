using Azure4Alexa.Alexa;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azure4Alexa.Sycamore.Models
{
    public class HomeworkAssignment
    {

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("ClassID")]
        public int ClassID { get; set; }

        [JsonProperty("ClassName")]
        public string ClassName { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("DueDate")]
        public string DueDate { get; set; }

        public string DueDateFormatted
        {
            get
            {
                return Convert.ToDateTime(DueDate).ToString(AlexaConstants.DefaultDateFormat);
            }
        }
    }

}