using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.AzureService;
using AzureRestAPI.Models;
using AzureRestAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace AzureRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //for model binding
    public class BooksController : ControllerBase
    {
        private readonly IDocumentDBRepository<Book> _documentDBRepository;
        private readonly ILogger _logger;
        private readonly LinkGenerator _linkGenerator;

        public BooksController(IDocumentDBRepository<Book> documentDBRepository, 
            LinkGenerator linkGenerator, ILogger<BooksController> logger)
        {
            _documentDBRepository = documentDBRepository;
            _logger = logger;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("entities")]
        public async Task<ActionResult<List<Book>>> GetEntitiesAsync()
        {
            return (await _documentDBRepository.GetItemsAsync(d => true)).ToList();            
        }

        [HttpGet("entities/{id}")]
        public async Task<ActionResult<Book>> GetEntityAsync(string id)
        {
            return await _documentDBRepository.GetItemAsync(id);            
        }

        [HttpPost("entities")]        
        public async Task<ActionResult<Book>> CreateAsync(Book item)
        {
            var getLink = _linkGenerator.GetPathByAction("GetEntityAsync", "Books", new { Id = item.Id });
            return Created(getLink, await _documentDBRepository.CreateItemAsync(item));
        }

        [HttpPut("entities/{id}")]
        public async Task<ActionResult<Book>> UpdateAsync(string id, Book item)
        {            
            var oldBook = await _documentDBRepository.UpdateItemAsync(id, item);
            if (oldBook == null)
            {
                return NotFound($"Could not find book with id of {id}");
            }
            return (Book)(dynamic) oldBook;
        }
    }
}
