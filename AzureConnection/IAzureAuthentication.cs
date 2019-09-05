using System.Threading.Tasks;

namespace AzureRestAPI.AzureConnection
{
    public interface IAzureAuthentication
    {
        Task<string> GetTokenAsync(string resourceUrl);        
    }
}