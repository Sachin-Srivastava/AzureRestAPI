using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AzureRestAPI.Repositories.Client
{
    public interface ICosmosClient
    {
        Task<Document> ReadDocumentAsync(string id);
        IDocumentQuery<T> CreateDocumentQuery<T>(Expression<Func<T, bool>> predicate);

        Task<Document> CreateDocumentAsync<T>(T item);

        Task<Document> ReplaceDocumentAsync<T>(string id, T item);

        Task<Document> DeleteDocumentAsync(string id);

        //Task<Document> ReadDatabaseAsync();

        //Task<> ReadDocumentCollectionAsync();
    }
}
