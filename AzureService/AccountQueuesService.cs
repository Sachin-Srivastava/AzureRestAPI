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
    public class AccountQueuesService : IAccountQueuesService
    {
        private readonly string _resourceUrl;
        private readonly IAzureClient _azureClient;
        private readonly AppSettings _config;

        public AccountQueuesService (IOptions<AppSettings> config, IAzureClient azureClient,
                                        SharedResources resources)
        {
            _azureClient = azureClient;
            _config = config.Value;
            _resourceUrl = resources.QueueUrl("ssactivateazure");
        }
        public async Task<AzureQueueList> GetAccountQueues()
        {
            var content = await _azureClient.AzureGetResource($"{_resourceUrl}?comp=list", _resourceUrl);            
            XmlSerializer serializer = new XmlSerializer(typeof(AzureEnumerationResults));
            AzureEnumerationResults result;
            using (TextReader reader = new StringReader(content))
            {
                 result = (AzureEnumerationResults) serializer.Deserialize(reader);

            }
            //Console.WriteLine(content);
            return result.accountQueueLists[0];
        }
    }
}