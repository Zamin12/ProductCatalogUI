using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogUI.Models
{
    public class CreateProductViewModel
    {
        [Required]
        [StringLength(10)]
        [Remote(action: "IsCodeUnique", controller: "ProductCatalog")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Upload File")]
        public IFormFile PhotoFile { get; set; }

        [Required]
        [Range(0.01, 99999999.99, ErrorMessage = "The Price must be in correct range. Maximum 8 digits and 2 decimal digits.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "The Price must be in correct format. Maximum 2 decimal digits.")]
        public decimal Price { get; set; }
    }
}
