using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogUI.Infrastructure.HttpMessageHandlers
{
    public class EnsureSuccessHttpMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
              HttpRequestMessage request,
              CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
