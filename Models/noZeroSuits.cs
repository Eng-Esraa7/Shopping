using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class noZeroSuits:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var suits = (suits)validationContext.ObjectInstance;
            return (suits.oldPrice == 0 || suits.newPrice == 0) ? new ValidationResult("Number Should Greater Than 0") : ValidationResult.Success;
        }
    }
}