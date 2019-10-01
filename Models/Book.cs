using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AzureRestAPI.Models
{
    public class Book
    {
        //[JsonProperty(PropertyName = "id")]             
        public string Id { get; set; }

        //[JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        //[JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        //[JsonProperty(PropertyName = "price")]
        public int Price { get; set; }
    }
}