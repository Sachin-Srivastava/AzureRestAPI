using System.Collections.Generic;
using System.Xml.Serialization;

namespace AzureRestAPI.AzureDTO
{
     [XmlRoot("Queues")]
    public class AzureQueueList
    {
         [XmlElement("Queue")]
        public List<AzureQueue> lQueue {get; set;}
    }
}