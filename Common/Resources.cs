namespace AzureRestAPI.Common
{
    public class SharedResources
    {
        public string ManagementUrl { get; } = "https://management.azure.com/";
        private string _queueUrl = "https://{0}.queue.core.windows.net";

        public string QueueUrl(string accountName)
        {
            return string.Format(_queueUrl, accountName);
        }
    }
}