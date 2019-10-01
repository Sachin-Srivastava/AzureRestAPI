using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AzureRestAPI.Models;
using Microsoft.Azure.Documents;

namespace AzureRestAPI.Repositories
{
    public interface IDocumentDBRepository<T> where T : class
    {
        Task<T> CreateItemAsync(T item);
        Task DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate);
        Task<T> UpdateItemAsync(string id, T item);
    }
}