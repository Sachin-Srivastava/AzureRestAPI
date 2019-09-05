using Newtonsoft.Json;

namespace AzureRestAPI.AzureDTO
{
    public class StorageAccount
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}