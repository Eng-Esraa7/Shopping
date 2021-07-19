using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class noZeroShoeses:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var shirts = (shoes)validationContext.ObjectInstance;
            return (shirts.oldPrice == 0 || shirts.newPrice == 0 || shirts.Size==0) ? new ValidationResult("Number Should Greater Than 0") : ValidationResult.Success;
        }
    }
}