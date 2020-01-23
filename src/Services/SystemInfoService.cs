using ProductCatalogUI.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductCatalogUI.Services
{
    public class SystemInfoService : BaseHttpClientService, ISystemInfoService
    {
        public SystemInfoService(HttpClient httpClient) : base(httpClient) { }

        public async Task<string> GetSystemInfo() => await Get<string>("");
    }
}
