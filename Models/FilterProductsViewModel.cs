using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogUI.Models
{
    public class FilterProductsViewModel
    {
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Range(0.01, 99999999.99, ErrorMessage = "The Price must be in correct range. Maximum 8 digits and 2 decimal digits.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "The Price must be in correct format. Maximum 2 decimal digits.")]
        public decimal? StartPrice { get; set; }

        [Range(0.01, 99999999.99, ErrorMessage = "The Price must be in correct range. Maximum 8 digits and 2 decimal digits.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "The Price must be in correct format. Maximum 2 decimal digits.")]
        public decimal? EndPrice { get; set; }
    }
}
