using Newtonsoft.Json;


namespace Azure4Alexa.Sycamore.Models
{
    public class Account
    {

        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Amount")]
        public string Amount { get; set; }
    }
}