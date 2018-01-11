using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azure4Alexa.Sycamore.Models
{
    public class MenuItem
    {
        [JsonProperty("MealID")]
        public int MealID { get; set; }

        [JsonProperty("MealName")]
        public string MealName { get; set; }

        [JsonProperty("MealDesc")]
        public string MealDesc { get; set; }
    }
}