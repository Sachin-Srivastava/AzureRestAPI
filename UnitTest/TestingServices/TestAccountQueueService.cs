using System;
using Xunit;
using AzureRestAPI.AzureService;
using Microsoft.Extensions.Options;
using AzureRestAPI.Common;
using System.Threading.Tasks;
using AzureRestAPI.AzureConnection;
using NSubstitute;
using System.Net.Http;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using AzureRestAPI.AzureDTO;
using System.Collections.Generic;

namespace AzureRestAPI.Tests
{
    public class TestAccountQueueService
    {
        [Fact]
        public async Task StorageAccountServiceTest_GetReturnsCorrectAccount()
        {
            var appSettings = new AppSettings();
            appSettings.TokenUrl = "http://good.uri";
            var config = Options.Create(appSettings);
            var resources = new SharedResources();
            
            var mockAzureClient = Substitute.For<IAzureClient>();
            var queueContent = "<EnumerationResults><Queues></Queues></EnumerationResults>";
            mockAzureClient.AzureGetResource(Arg.Any<string>(), Arg.Any<string>())
                            .Returns(Task.FromResult(queueContent));
            var storageAccountService = new AccountQueuesService(config, mockAzureClient, resources);
            var accounts = await storageAccountService.GetAccountQueues();  
            accounts.lQueue.Count.Equals(0);            
        }
    }
}
