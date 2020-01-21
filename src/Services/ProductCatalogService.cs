using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ProductCatalogUI.Infrastructure;
using ProductCatalogUI.Models;

namespace ProductCatalogUI.Services
{
    public class ProductCatalogService : BaseHttpClientService, IProductCatalogService
    {
        private readonly HttpClient _httpClient;

        public ProductCatalogService(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetAllProducts() => await Get<IEnumerable<Product>>("Products");

        public async Task<IEnumerable<Product>> GetProductsByFilter(string filterStr) => await Get<IEnumerable<Product>>($"Products/Filter{filterStr}");

        public async Task<Product> GetProduct(Guid id) => await Get<Product>($"Products/{id}");

        public async Task<Product> CreateProduct(Product product) => await Post<Product, Product>($"Products", product);

        public async Task<Product> UpdateProduct(Guid id, Product product) => await Put<Product, Product>($"Products/{id}", product);

        public async Task DeleteProduct(Guid id) => await Delete($"Products/{id}");
    }
}