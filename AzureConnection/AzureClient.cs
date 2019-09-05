using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;
using System.Globalization;

namespace AzureRestAPI.AzureConnection
{
    public class AzureClient : IAzureClient
    {        
        private readonly HttpClient _client;
        private readonly IAzureAuthentication _azureAuthentication;
        public AzureClient(IAzureAuthentication azureAuthentication, IHttpClientFactory httpClientFactory)
        {            
            _client = httpClientFactory.CreateClient();
            _azureAuthentication = azureAuthentication;
        }
        public async Task<string> AzureGet(string url, string resourceUrl)
        {
            var token = await _azureAuthentication.GetTokenAsync(resourceUrl);
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _client.SendAsync(requestMessage);
                return await response.Content.ReadAsStringAsync();
            }            
        }
        
        public async Task<string> AzureGetResource(string url, string resourceUrl)
        {
            var token = await _azureAuthentication.GetTokenAsync(resourceUrl);
            var now = DateTime.UtcNow;
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                requestMessage.Headers.Add("x-ms-date", now.AddHours(1).ToString("R", CultureInfo.InvariantCulture));
                requestMessage.Headers.Add("x-ms-version", "2018-11-09");
                var response = await _client.SendAsync(requestMessage);
                return await response.Content.ReadAsStringAsync();
            }             
        }
    }
}