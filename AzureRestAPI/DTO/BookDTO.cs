using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureRestAPI.DTO
{
    public class BookDTO
    {
        public string Name { get; set; }
        
        public string Category { get; set; }
        
        public int Price { get; set; }
    }
}
