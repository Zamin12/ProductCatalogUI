using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogUI.Infrastructure
{
    public class BaseHttpClientService
    {
        private readonly HttpClient _httpClient;

        protected internal BaseHttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region Custom Methods

        public Task<TResponse> Post<TRequest, TResponse>(string requestUrl, TRequest requestValue)
            => SendRequestAsync<TRequest, TResponse>(HttpMethod.Post, requestUrl, requestValue);

        public Task Post<TRequest>(string requestUrl, TRequest requestValue)
            => SendRequestAsync<TRequest>(HttpMethod.Post, requestUrl, requestValue);


        public Task<TResponse> Get<TRequest, TResponse>(string requestUrl, TRequest requestValue)
            => SendRequestAsync<TRequest, TResponse>(HttpMethod.Get, requestUrl, requestValue);

        public Task<TResponse> Get<TResponse>(string requestUrl)
            => SendRequestAsync<TResponse>(HttpMethod.Get, requestUrl);


        public Task<TResponse> Put<TRequest, TResponse>(string requestUrl, TRequest requestValue)
            => SendRequestAsync<TRequest, TResponse>(HttpMethod.Put, requestUrl, requestValue);

        public Task Delete(string requestUrl)
            => SendRequestAsync(HttpMethod.Delete, requestUrl);


        #endregion

        #region Implementations

        private async Task<TResponse> SendRequestAsync<TResponse>(HttpMethod method, string requestUrl)
        {
            var request = new HttpRequestMessage(method, requestUrl);

            var response = await _httpClient.SendAsync(request);

            return await DeserializeResponse<TResponse>(response);
        }

        private async Task SendRequestAsync<TRequest>(HttpMethod method, string requestUrl, TRequest requestValue)
        {
            var request = SerializeRequest(method, requestUrl, requestValue);

            await _httpClient.SendAsync(request);
        }

        private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(HttpMethod method, string requestUrl, TRequest requestValue)
        {
            var request = SerializeRequest(method, requestUrl, requestValue);

            var response = await _httpClient.SendAsync(request);

            return await DeserializeResponse<TResponse>(response);
        }

        private async Task SendRequestAsync(HttpMethod method, string requestUrl)
        {
            var request = new HttpRequestMessage(method, requestUrl);

            await _httpClient.SendAsync(request);
        }

        private HttpRequestMessage SerializeRequest<TRequest>(HttpMethod method, string requestUri, TRequest requestValue) => new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestValue), Encoding.UTF8, "application/json")
        };

        private async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage response) =>
            JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());

        #endregion
    }
}
