using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.AzureService;
using AzureRestAPI.DTO;
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
        private readonly IMapper _mapper;

        public BooksController(IDocumentDBRepository<Book> documentDBRepository, 
            LinkGenerator linkGenerator, ILogger<BooksController> logger, IMapper mapper)
        {
            _documentDBRepository = documentDBRepository;
            _logger = logger;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet("entities")]
        public async Task<ActionResult<List<BookDTO>>> GetEntitiesAsync()
        {
            var listItem =  (await _documentDBRepository.GetItemsAsync(d => true)).ToList();
            return _mapper.Map<List<BookDTO>>(listItem);
        }

        [HttpGet("entities/{id}")]
        public async Task<ActionResult<BookDTO>> GetEntityAsync(string id)
        {
            var item = await _documentDBRepository.GetItemAsync(id);
            return _mapper.Map<BookDTO>(item);
        }

        [HttpPost("entities")]        
        public async Task<ActionResult<Book>> CreateAsync(BookDTO item)
        {
            var itemModel = _mapper.Map<Book>(item);
            var retModel = await _documentDBRepository.CreateItemAsync(itemModel);
            var getLink = _linkGenerator.GetPathByAction("GetEntityAsync", "Books", new { Id = retModel.Id });
            return Created(getLink, retModel);
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
