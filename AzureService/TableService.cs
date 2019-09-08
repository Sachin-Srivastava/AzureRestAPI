using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Xml.Serialization;
using System.IO;
using System;
using AzureRestAPI.AzureConnection;
using AzureRestAPI.Common;
using AzureRestAPI.AzureDTO;

namespace AzureRestAPI.AzureService
{
    public class TableService : ITableService
    {
        private readonly string _resourceUrl;
        private readonly IAzureClient _azureClient;
        private readonly AppSettings _config;

        public TableService (IOptions<AppSettings> config, IAzureClient azureClient,
                                        SharedResources resources)
        {
            _azureClient = azureClient;
            _config = config.Value;
            _resourceUrl = resources.QueueUrl("ssactivateazure");
        }
        public async Task<string> GetEntities()
        {
            return await _azureClient.AzureGetResource("https://ssactivateazure.table.core.windows.net/mytable", "https://management.azure.com/");        
        }
    }
}