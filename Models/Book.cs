using Newtonsoft.Json;

namespace AzureRestAPI.Models
{
    public class Book
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "price")]
        public int Completed { get; set; }
    }
}