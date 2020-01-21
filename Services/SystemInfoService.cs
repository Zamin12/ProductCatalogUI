using ProductCatalogUI.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductCatalogUI.Services
{
    public class SystemInfoService : BaseHttpClientService, ISystemInfoService
    {
        private readonly HttpClient _httpClient;

        public SystemInfoService(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetSystemInfo() => await Get<string>("");
    }
}
