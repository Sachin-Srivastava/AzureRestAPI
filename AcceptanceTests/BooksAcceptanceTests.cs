using AzureRestAPI.Controllers;
using AzureRestAPI.Models;
using AzureRestAPI.Repositories;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace AcceptanceTests
{
    public class BooksAcceptanceTests
    {
        [Fact(Skip ="not yet completed")]
        public void CreateANewBook()
        {
            //IDocumentDBRepository<Book> documentDBRepository = new DocumentDBRepository();
            
            //var booksController = new BooksController(documentDBRepository,
            //null, null);

        }
    }
}
