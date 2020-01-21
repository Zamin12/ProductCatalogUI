using Newtonsoft.Json;
using ProductCatalogUI.Models;
using ProductCatalogUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class ProductCatalogServiceTest
    {
        [Fact]
        public async Task GetAllProducts_ReturnsDataAsync()
        {
            // Arrange
            var productList = new List<Product>() {
                new Product() { Id= Guid.NewGuid(), Code = "123"},
                new Product() { Id= Guid.NewGuid(), Code = "234"}
            };

            var httpClient = new TestHelper().CreateMockHttpClient(JsonConvert.SerializeObject(productList), HttpStatusCode.OK);

            var service = new ProductCatalogService(httpClient);

            // Act
            var result = await service.GetAllProducts();

            // Assert
            Assert.Equal(productList.Count(), result.Count());
        }

        [Fact]
        public async Task GetAllProducts_ReturnsNoDataAsync()
        {
            // Arrange
            var productList = new List<Product>() { };

            var httpClient = new TestHelper().CreateMockHttpClient(JsonConvert.SerializeObject(productList), HttpStatusCode.OK);

            var service = new ProductCatalogService(httpClient);

            // Act
            var result = await service.GetAllProducts();

            // Assert
            Assert.Equal(productList.Count(), result.Count());
        }

        [Fact]
        public async Task GetProductsByFilter_ReturnsDataAsync()
        {
            // Arrange
            var code = "123";
            var productList = new List<Product>() {
                new Product() { Id= Guid.NewGuid(), Code = code}
            };

            var httpClient = new TestHelper().CreateMockHttpClient(JsonConvert.SerializeObject(productList), HttpStatusCode.OK);

            var service = new ProductCatalogService(httpClient);

            // Act
            var result = await service.GetProductsByFilter($"Code={code}");

            // Assert
            Assert.Equal(productList.Count(), result.Count());
            Assert.Equal(code, result.First().Code);
        }

        [Fact]
        public async Task GetProductById_ReturnsDataAsync()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new Product() { Id = id, Code = "123" };

            var httpClient = new TestHelper().CreateMockHttpClient(JsonConvert.SerializeObject(product), HttpStatusCode.OK);

            var service = new ProductCatalogService(httpClient);

            // Act
            var result = await service.GetProduct(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFoundAsync()
        {
            // Arrange
            var httpClient = new TestHelper().CreateMockHttpClient("", HttpStatusCode.NotFound);

            var service = new ProductCatalogService(httpClient);

            // Act
            var result = await service.GetProduct(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateProduct_CreatedAsync()
        {
            // Arrange
            var code = "123";
            var product = new Product() { Code = code };

            var httpClient = new TestHelper().CreateMockHttpClient(JsonConvert.SerializeObject(product), HttpStatusCode.Created);

            var service = new ProductCatalogService(httpClient);

            // Act
            var result = await service.CreateProduct(product);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(code, result.Code);
        }

        [Fact]
        public async Task UpdateProduct_UpdatedAsync()
        {
            // Arrange
            var code = "123";
            var product = new Product() { Code = code };

            var httpClient = new TestHelper().CreateMockHttpClient("", HttpStatusCode.NoContent);

            var service = new ProductCatalogService(httpClient);

            // Act
            var result = await service.UpdateProduct(Guid.NewGuid(), product);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public void DeleteProduct_Deleted()
        {
            // Arrange
            var code = "123";
            var product = new Product() { Code = code };

            var httpClient = new TestHelper().CreateMockHttpClient("", HttpStatusCode.OK);

            var service = new ProductCatalogService(httpClient);

            // Act
            async Task Action() => await service.DeleteProduct(Guid.NewGuid());

            // Assert
            Assert.Null(Action().Exception);
        }

        [Fact]
        public void DeleteProduct_ReturnsNotFound()
        {
            // Arrange
            var code = "123";
            var product = new Product() { Code = code };

            var httpClient = new TestHelper().CreateMockHttpClient(JsonConvert.SerializeObject(product), HttpStatusCode.Created);

            var service = new ProductCatalogService(httpClient);

            // Act
            async Task Action() => await service.DeleteProduct(Guid.NewGuid());

            // Assert
            Assert.Null(Action().Exception);
        }
    }
}
