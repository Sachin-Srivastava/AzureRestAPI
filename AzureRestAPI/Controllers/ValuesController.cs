using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.AzureService;
using Microsoft.AspNetCore.Mvc;

namespace AzureRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAccountQueuesService _accountQueueService;
        private readonly IStorageAccountService _storageAccountService;
        public ValuesController(IAccountQueuesService accountQueueService,
            IStorageAccountService storageAccountService )
        {
            _accountQueueService = accountQueueService;
            _storageAccountService = storageAccountService;
        }
        // GET api/values
        [Route("queues")]
        [HttpGet]
        public async Task<ActionResult<AzureQueueList>> GetQueuesAsync()
        {                      
            return await _accountQueueService.GetAccountQueues();
        }

        [Route("accounts")]
        [HttpGet]
        public async Task<ActionResult<StorageAccountList>> GetAccountsAsync()
        {
            return await _storageAccountService.GetStorageAccounts();            
        }
    }
}
