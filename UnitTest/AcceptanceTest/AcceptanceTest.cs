using Microsoft.Azure.Documents;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Threading.Tasks;
using Xunit;
using AzureRestAPI.Models;
using AzureRestAPI.Repositories;
using Microsoft.Azure.Documents.Client;
using FluentAssertions;
using Microsoft.Azure.Documents.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using AzureRestAPI.Repositories.Client;
using AzureRestAPI.Controllers;

namespace AcceptanceTest
{
    public class AcceptanceTest
    {
        private string _endPoint = "https://ss-activateazure.documents.azure.com:443/";
        private string _key = "nXctaHKDZwgabHbLTO5rVZQHQv4LZuy3mv6gNY8AggHTb8jyYTTiuKkDeM5yQD7ZqHZoqRHVcl2mOTloBODIsw==";

        [Fact]
        public async Task DocumentDBRepositoryTest_GetItemAsync_ReturnsItem()
        {
            IDocumentClient documentClient = new DocumentClient(new Uri(_endPoint), _key);
            var cosmosClient = new CosmosClient(documentClient);
            var documentDBRepository = new DocumentDBRepository<Book>(cosmosClient, documentClient);
            var document = await documentDBRepository.CreateItemAsync(new Book());

            var documentReturned = await documentDBRepository.GetItemAsync(document.Id);
            documentReturned.Id.Should().Equals(document.Id);

            await documentDBRepository.DeleteItemAsync(document.Id);

            documentReturned.Should().NotBeNull();
        }
        
    }
}
