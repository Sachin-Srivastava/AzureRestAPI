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

namespace UnitTest
{
    public class TestDocumentDBRepository
    {
        public interface IFakeDocumentQuery<T> : IDocumentQuery<T>, IOrderedQueryable<T>
        {

        }

        [Fact]
        public async Task DocumentDBRepositoryTest_GetItemAsync_ReturnsItem()
        {
            var mockDocumentClient = Substitute.For<IDocumentClient>();
            mockDocumentClient.ReadDatabaseAsync(Arg.Any<string>())
                            .ReturnsNull();
            mockDocumentClient.ReadDocumentAsync(Arg.Any<Uri>())
                            .Returns(Task.FromResult(new ResourceResponse<Document>(new Document())));
            var documentDBRepository = new DocumentDBRepository<Book>(mockDocumentClient);
            var document = await documentDBRepository.GetItemAsync("1");

            document.Should().NotBeNull();
        }

        [Fact(Skip = "reason")]
        public async Task DocumentDBRepositoryTest_PostItemAsync_ReturnsItem()
        {
            var mockDocumentClient = Substitute.For<IDocumentClient>();
            mockDocumentClient.ReadDatabaseAsync(Arg.Any<string>())
                            .ReturnsNull();
            mockDocumentClient.CreateDocumentAsync(Arg.Any<Uri>(), Arg.Any<Document>())
                            .Returns(Task.FromResult(new ResourceResponse<Document>(new Document())));
            var documentDBRepository = new DocumentDBRepository<Book>(mockDocumentClient);
            var document = await documentDBRepository.CreateItemAsync(new Book() { Name = "test"});

            document.Should().NotBeNull();
        }

        [Fact(Skip = "reason")]
        public async Task DocumentDBRepositoryTest_GetItemsAsync_ReturnsItemList()
        {
            var mockDocumentClient = Substitute.For<IDocumentClient>();
            //var mockQuery = Substitute.For<IDocumentQuery<Document>>();

            //var mockOrderedQuery = Substitute.For<IOrderedQueryable<Book>>();
            var mockQueryable = Substitute.For<IOrderedQueryable<Book>>();
            var mockDocumentQuery = Substitute.For<IDocumentQuery<Book>, IQueryable<Book>>();

            mockDocumentClient.ReadDatabaseAsync(Arg.Any<string>())
                            .ReturnsNull();
            var emptyLst = new List<Book>()
            {
                new Book()
                {
                    Name = "this"
                },
                new Book()
                {
                    Name = "that"
                }
            };

            //IOrderedQueryable<Book> queryable = emptyLst.AsQueryable().OrderBy(t => t.Name);

            //mockDocumentQuery.Where(Arg.Any<Expression<Func<Book, bool>>>()).Returns(queryable);
            mockDocumentQuery.HasMoreResults.Returns(true, false);
            mockDocumentQuery.ExecuteNextAsync<Book>().Returns(new FeedResponse<Book>(emptyLst));

            mockQueryable.AsDocumentQuery().Returns(mockDocumentQuery);
            //mockOrderedQuery.Where<Book>(Arg.Any<Expression<Func<Book, bool>>>()).Returns(mockQueryable);
            

            //IFakeDocumentQuery<Book> lst = emptyLst.AsQueryable().OrderBy(x => x.Id);
            //IOrderedQueryable<Book> books = new List<Book>();
            //IFakeDocumentQuery<Book> lst = (IFakeDocumentQuery<Book>)emptyLst.AsQueryable();
            //IFakeDocumentQuery<Book> fakeQuery = queryable;
            mockDocumentClient.CreateDocumentQuery<Book>(Arg.Any<Uri>(), Arg.Any<FeedOptions>()).Returns(mockQueryable);
            var documentDBRepository = new DocumentDBRepository<Book>(mockDocumentClient);
            var document = await documentDBRepository.GetItemsAsync(d => true);

            document.Should().NotBeNull();
        }
    }
}
