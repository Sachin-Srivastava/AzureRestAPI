using System.Net.Http;
using System.Threading.Tasks;
using AzureRestAPI.AzureConnection;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AzureRestAPI.AzureService
{
    public class StorageAccountService : IStorageAccountService
    {
        private readonly IAzureClient _azureClient;
        private readonly AppSettings _config;
        private readonly string _resourceUrl;
        public StorageAccountService (IOptions<AppSettings> config, IAzureClient azureClient,
                                    SharedResources resources)
        {
            _azureClient = azureClient;
            _config = config.Value;
            _resourceUrl = resources.ManagementUrl;
        }
        public async Task<StorageAccountList> GetStorageAccounts()
        {
            var content = await _azureClient.AzureGet($"{_resourceUrl}/subscriptions/{_config.SubscriptionId}/providers/Microsoft.Storage/storageAccounts?api-version=2019-04-01", _resourceUrl);
            return await content.ReadAsAsync<StorageAccountList>();
            //return JsonConvert.DeserializeObject<StorageAccountList>(content);
        }
    }
}