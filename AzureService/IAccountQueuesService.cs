using AzureRestAPI.AzureDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureRestAPI.AzureService
{
    public interface IAccountQueuesService
    {
        Task<AzureQueueList> GetAccountQueues();
    }
}