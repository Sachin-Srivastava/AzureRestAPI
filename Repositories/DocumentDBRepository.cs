using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AzureRestAPI.Models;
using AzureRestAPI.Repositories.Client;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace AzureRestAPI.Repositories
{

    public class DocumentDBRepository<T> : IDocumentDBRepository<T> where T : class
    {
        private readonly string DatabaseId = "BookStore";
        private readonly string CollectionId = "Book";
        //private readonly IDocumentClient _client;
        private readonly ICosmosClient _client;
        private readonly IDocumentClient _docClient;

        public DocumentDBRepository(ICosmosClient client, IDocumentClient docClient)
        {
            _client = client;
            _docClient = docClient;
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await _client.ReadDocumentAsync(id);
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = _client.CreateDocumentQuery(predicate);

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task<T> CreateItemAsync(T item)
        {
            return (T) (dynamic) await _client.CreateDocumentAsync(item);
        }

        public async Task<T> UpdateItemAsync(string id, T item)
        {
            return (T)(dynamic) await _client.ReplaceDocumentAsync(id, item);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _client.DeleteDocumentAsync(id);
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await _docClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _docClient.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await _docClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _docClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}