using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogUI.Models
{
    public class EditProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        [Remote(action: "IsCodeUnique", controller: "ProductCatalog", AdditionalFields = nameof(Id))]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Upload File (Leaving empty will delete picture)")]
        public IFormFile PhotoFile { get; set; }

        public string Photo { get; set; }

        [Required]
        [Range(0.01, 99999999.99, ErrorMessage = "The Price must be in correct range. Maximum 8 digits and 2 decimal digits.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "The Price must be in correct format. Maximum 2 decimal digits.")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
    }
}
