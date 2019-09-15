using Xunit;
using AzureRestAPI.AzureService;
using Microsoft.Extensions.Options;
using AzureRestAPI.Common;
using System.Threading.Tasks;
using AzureRestAPI.AzureConnection;
using NSubstitute;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace AzureRestAPI.Tests.TestingAzureConnection
{
    public class TestAzureClient
    {
        [Fact]
        public async Task AzureClientTest_AzureGetReturnsContent()
        {
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage() {
                                            StatusCode = HttpStatusCode.OK,
                                            Content = new StringContent(JsonConvert.SerializeObject("Testing"), 
                                            Encoding.UTF8, "application/json") 
                                        });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            
            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);
            var azureAuthentication = Substitute.For<IAzureAuthentication>();
            azureAuthentication.GetTokenAsync(Arg.Any<string>()).Returns(Task.FromResult("testToken"));

            var azureClient = new AzureClient(azureAuthentication, httpClientFactoryMock);
            var content = await azureClient.AzureGet("http://good.uri","http://good.uri");
            content.Equals("Testing");
        }
        [Fact]
        public async Task AzureClientTest_AzureGetResourceReturnsContent()
        {
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage() {
                                            StatusCode = HttpStatusCode.OK,
                                            Content = new StringContent(JsonConvert.SerializeObject("Testing"), 
                                            Encoding.UTF8, "application/json") 
                                        });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            
            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);
            var azureAuthentication = Substitute.For<IAzureAuthentication>();
            azureAuthentication.GetTokenAsync(Arg.Any<string>()).Returns(Task.FromResult("testToken"));

            var azureClient = new AzureClient(azureAuthentication, httpClientFactoryMock);
            var content = await azureClient.AzureGetResource("http://good.uri","http://good.uri");
            content.Equals("Testing");
        }
    }
}