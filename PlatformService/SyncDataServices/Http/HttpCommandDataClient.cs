using Newtonsoft.Json;
using PlatformService.Dtos;
using System.Text;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<SalesTracker>?> GetSalesTracker()
        {
            var url = $"{_config["CommandService"]}{_config["SalesTrackerEndpoint"]}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<List<SalesTracker>>(responseBody);
                return obj;
            }
            return null;
        }

        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var url = $"{_config["CommandService"]}{_config["CommandServiceInboundEndpoint"]}";
            var httpContent = new StringContent(
                    JsonConvert.SerializeObject(platform),
                    Encoding.UTF8,
                    "application/json"
                );

            var response = await _httpClient.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Command Service was Okay!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to Command Service was no Okay!");

            }
        }
    }
}
