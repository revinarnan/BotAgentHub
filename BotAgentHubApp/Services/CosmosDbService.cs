using BotAgentHubApp.Models;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BotAgentHubApp.Services
{
    public class CosmosDbService
    {
        private static readonly string Endpoint = WebConfigurationManager.AppSettings["CosmosDbEndpoint"];
        private static readonly string Key = WebConfigurationManager.AppSettings["CosmosDbAuthKey"];
        private static readonly string DatabaseName = WebConfigurationManager.AppSettings["CosmosDbDatabaseId"];
        private static readonly string ContainerName = WebConfigurationManager.AppSettings["CosmosDbContainerId"];

        public async Task<CosmosDto> GetItemAsync(string id)
        {
            CosmosClient client = new CosmosClient(accountEndpoint: Endpoint, authKeyOrResourceToken: Key);
            Database database = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);
            Container container = await database.CreateContainerIfNotExistsAsync(
                id: ContainerName,
                partitionKeyPath: "/id");

            return await container.ReadItemAsync<CosmosDto>(id, new PartitionKey(id));
        }
    }
}