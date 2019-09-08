using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.AzureService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAccountQueuesService _accountQueueService;
        private readonly IStorageAccountService _storageAccountService;
        private readonly ITableService _tableService;
        private readonly IDocumentDBRepository<Item> _documentDBRepository;
        private readonly ILogger _logger;

        public ValuesController(IAccountQueuesService accountQueueService,
            IStorageAccountService storageAccountService, ITableService tableService,
            IDocumentDBRepository<Item> documentDBRepository, ILogger<ValuesController> logger)
        {
            _accountQueueService = accountQueueService;
            _storageAccountService = storageAccountService;
            _tableService = tableService;
            _documentDBRepository = documentDBRepository;
            _logger = logger;
        }
        // GET api/values
        [Route("queues")]
        [HttpGet]
        public async Task<ActionResult<AzureQueueList>> GetQueuesAsync()
        {
            _logger.LogInformation("Here");
            _logger.LogError("Here");
            //Console.WriteLine("Here");
            //System.Diagnostics.Debug.WriteLine("Here");
            return await _accountQueueService.GetAccountQueues();
        }

        [Route("accounts")]
        [HttpGet]
        public async Task<ActionResult<StorageAccountList>> GetAccountsAsync()
        {
            return await _storageAccountService.GetStorageAccounts();            
        }

        [Route("entities")]
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetEntitiesAsync()
        {
            var x = await _documentDBRepository.GetItemsAsync(d => !d.Completed);
            return x.ToList();
            //return x.ToString();
        }
    }
}
