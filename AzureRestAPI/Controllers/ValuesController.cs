using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.AzureService;
using AzureRestAPI.Models;
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
        private readonly IDocumentDBRepository<Book> _documentDBRepository;
        private readonly ILogger _logger;

        public ValuesController(IAccountQueuesService accountQueueService,
            IStorageAccountService storageAccountService, ITableService tableService,
            IDocumentDBRepository<Book> documentDBRepository, ILogger<ValuesController> logger)
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
        
        [HttpGet("entities")]
        public async Task<ActionResult<List<Book>>> GetEntitiesAsync()
        {
            var x = await _documentDBRepository.GetItemsAsync(d => true);
            return Ok(x.ToList());
            //return x.ToString();
        }

        //[HttpPost]
        [HttpPost("entities")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Book>> CreateAsync(Book item)
        {
            if (ModelState.IsValid)
            {
                await _documentDBRepository.CreateItemAsync(item);
                //return RedirectToAction("Index");
            }

            return Ok(item);
        }
    }
}
