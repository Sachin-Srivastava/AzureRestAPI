using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace AzureRestAPI.Repositories.Client
{
    public class CosmosClient : ICosmosClient
    {
        private readonly string DatabaseId = "BookStore";
        private readonly string CollectionId = "Book";
        private readonly IDocumentClient _client;
        public CosmosClient(IDocumentClient client)
        {
            _client = client;
        }
        public async Task<Document> CreateDocumentAsync<T>(T item)
        {
            return await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
        }

        public IDocumentQuery<T> CreateDocumentQuery<T>(Expression<Func<T, bool>> predicate)
        {
            return _client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();
        }

        public async Task<Document> DeleteDocumentAsync(string id)
        {
            return await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
        }

        public async Task<Document> ReadDocumentAsync(string id)
        {
            return await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
        }

        public async Task<Document> ReplaceDocumentAsync<T>(string id, T item)
        {
            return await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id), item);
        }
    }
}
