using ProductCatalogUI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalogUI.Services
{
    public interface IProductCatalogService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProductsByFilter(string filterStr);
        Task<Product> GetProduct(Guid id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Guid id, Product product);
        Task DeleteProduct(Guid id);
    }
}