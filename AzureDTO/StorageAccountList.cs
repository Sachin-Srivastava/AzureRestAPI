using System.Collections.Generic;
using Newtonsoft.Json;

namespace AzureRestAPI.AzureDTO
{
    public class StorageAccountList
    {
        [JsonProperty("value")]
        public IList<StorageAccount> accountList {get; set;}
    }
}