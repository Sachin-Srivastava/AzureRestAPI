using System.Xml.Serialization;

namespace AzureRestAPI.AzureDTO
{
    public class AzureQueue
    {
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}