using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.AzureService;
using AzureRestAPI.Models;
using AzureRestAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureRestController : ControllerBase
    {
        private readonly IAccountQueuesService _accountQueueService;
        private readonly IStorageAccountService _storageAccountService;
        private readonly ITableService _tableService;        
        private readonly ILogger _logger;

        public AzureRestController(IAccountQueuesService accountQueueService,
            IStorageAccountService storageAccountService, ITableService tableService,
            IDocumentDBRepository<Book> documentDBRepository, ILogger<AzureRestController> logger)
        {
            _accountQueueService = accountQueueService;
            _storageAccountService = storageAccountService;
            _tableService = tableService;            
            _logger = logger;
        }
        // GET api/values
        [Route("queues")]
        [HttpGet]
        public async Task<ActionResult<AzureQueueList>> GetQueuesAsync()
        {
            _logger.LogInformation("Here");
            _logger.LogError("Here");            
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
