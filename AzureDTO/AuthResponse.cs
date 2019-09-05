using Newtonsoft.Json;

namespace AzureRestAPI.AzureDTO
{
    public class AuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}