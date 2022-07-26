using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BotAgentHubApp.Services
{
    public class AzureBlobService
    {
        private static readonly string connectionString = WebConfigurationManager.AppSettings["AzureTableStorageConnectionString"];
        private static readonly string containerName = WebConfigurationManager.AppSettings["ConversationHistoryContainerName"];

        public async Task DownloadTranscriptAsync(string id, string downloadPath)
        {
            // Get a reference to a container
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

            // Get a reference to a blob file in a container
            BlockBlobClient transcriptBlob = container.GetBlockBlobClient(id);

            await transcriptBlob.DownloadToAsync(downloadPath);
        }
    }
}