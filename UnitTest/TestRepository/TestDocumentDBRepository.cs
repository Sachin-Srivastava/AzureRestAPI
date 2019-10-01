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

namespace UnitTest
{
    public class TestDocumentDBRepository
    {

        [Fact]
        public async Task DocumentDBRepositoryTest_GetItemAsync_ReturnsItem()
        {
            var mockDocumentClient = Substitute.For<IDocumentClient>();
            var mockCosmosClient = Substitute.For<ICosmosClient>();
            mockDocumentClient.ReadDatabaseAsync(Arg.Any<string>())
                            .ReturnsNull();
            mockCosmosClient.ReadDocumentAsync(Arg.Any<string>())
                            .Returns(Task.FromResult(new Document()));
            var documentDBRepository = new DocumentDBRepository<Book>(mockCosmosClient, mockDocumentClient);
            var document = await documentDBRepository.GetItemAsync("1");

            document.Should().NotBeNull();
        }

        [Fact]
        public async Task DocumentDBRepositoryTest_PostItemAsync_ReturnsItem()
        {
            var mockDocumentClient = Substitute.For<IDocumentClient>();
            var mockCosmosClient = Substitute.For<ICosmosClient>();
            mockDocumentClient.ReadDatabaseAsync(Arg.Any<string>())
                            .ReturnsNull();
            mockCosmosClient.CreateDocumentAsync(Arg.Any<Book>())
                            .Returns(Task.FromResult(new Document()));
            var documentDBRepository = new DocumentDBRepository<Book>(mockCosmosClient, mockDocumentClient);
            var document = await documentDBRepository.CreateItemAsync(new Book() { Name = "test"});

            document.Should().NotBeNull();
        }

        [Fact]
        public async Task DocumentDBRepositoryTest_GetItemsAsync_ReturnsItemList()
        {
            var mockDocumentClient = Substitute.For<IDocumentClient>();
            var mockCosmosClient = Substitute.For<ICosmosClient>();
            mockDocumentClient.ReadDatabaseAsync(Arg.Any<string>())
                            .ReturnsNull();
            var mockDocumentQuery = Substitute.For<IDocumentQuery<Book>>();
            mockDocumentQuery.HasMoreResults.Returns(true, false);
            mockDocumentQuery.ExecuteNextAsync<Book>()
                .Returns(Task.FromResult(new FeedResponse<Book>(new List<Book>())));
            mockCosmosClient.CreateDocumentQuery<Book>(Arg.Any<Expression<Func<Book, bool>>>())
                            .Returns(mockDocumentQuery);
            var documentDBRepository = new DocumentDBRepository<Book>(mockCosmosClient, mockDocumentClient);

            var items = await documentDBRepository.GetItemsAsync(t => true);   
            items.Should().NotBeNull();
        }
    }
}
