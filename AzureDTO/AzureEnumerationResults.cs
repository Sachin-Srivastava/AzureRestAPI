using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AzureRestAPI.AzureDTO
{
    [XmlRoot("EnumerationResults")]
    public class AzureEnumerationResults
    {
        [XmlElement("Queues")]
        public List<AzureQueueList> accountQueueLists;
    }
}
