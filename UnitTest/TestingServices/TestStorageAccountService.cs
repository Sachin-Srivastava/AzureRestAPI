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
    public class TestStorageAccountService
    {
        [Fact]
        public async Task StorageAccountServiceTest_GetReturnsCorrectAccount()
        {
            var appSettings = new AppSettings();
            appSettings.TokenUrl = "http://good.uri";
            var config = Options.Create(appSettings);
            var resources = new SharedResources();            
            var mockAzureClient = Substitute.For<IAzureClient>();
            var storageAccountList = new StorageAccountList();
            storageAccountList.accountList = new List<StorageAccount>(){
                new StorageAccount(){
                    Kind = "testkind"
                }
            };
            mockAzureClient.AzureGet(Arg.Any<string>(), Arg.Any<string>())
                            .Returns(Task.FromResult((HttpContent)new StringContent(JsonConvert.SerializeObject(storageAccountList), Encoding.UTF8, "application/json")));
            var storageAccountService = new StorageAccountService(config, mockAzureClient, resources);
            var accounts = await storageAccountService.GetStorageAccounts();  
            accounts.accountList[0].Kind.Equals("testkind");            
        }
    }
}
