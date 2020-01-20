using Microsoft.AspNetCore.Http;
using ProductCatalogUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProductCatalogUI.Factories
{
    public static class ProductFactory
    {
        public static Product Make(CreateProductViewModel vm)
        {
            return new Product
            {
                Code = vm.Code,
                Name = vm.Name,
                Price = vm.Price,
                Photo = IFormFileToBase64String(vm.PhotoFile)
            };
        }

        public static Product Make(EditProductViewModel vm)
        {
            return new Product
            {
                Id = vm.Id,
                Code = vm.Code,
                Name = vm.Name,
                Price = vm.Price,
                Photo = IFormFileToBase64String(vm.PhotoFile)
            };
        }

        public static EditProductViewModel Make(Product model)
        {
            return new EditProductViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
                Price = model.Price,
                Photo = model.Photo
            };
        }

        public static string Make(FilterProductsViewModel vm)
        {
            if (vm == null) return "";

            List<string> conditions = new List<string>();
            if (!string.IsNullOrEmpty(vm.Code)) conditions.Add($"code={vm.Code}");
            if (!string.IsNullOrEmpty(vm.Name)) conditions.Add($"name={vm.Name}");
            if (vm.StartPrice > 0) conditions.Add($"startPrice={vm.StartPrice}");
            if (vm.EndPrice > 0) conditions.Add($"endPrice={vm.EndPrice}");

            var conditionString = string.Join("&", conditions);

            return !string.IsNullOrEmpty(conditionString) ? "?" + conditionString : "";
        }

        private static string IFormFileToBase64String(IFormFile formFile)
        {
            if (formFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    formFile.OpenReadStream().CopyTo(memoryStream);
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            else
                return null;
        }
    }
}
