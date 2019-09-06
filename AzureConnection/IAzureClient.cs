using System.Net.Http;
using System.Threading.Tasks;

namespace AzureRestAPI.AzureConnection
{
    public interface IAzureClient
    {
         Task<HttpContent> AzureGet(string url, string resourceUrl);
         Task<string> AzureGetResource(string url, string resourceUrl);
    }
}