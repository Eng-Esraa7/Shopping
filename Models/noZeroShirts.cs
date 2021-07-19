using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class noZeroShirts:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var shirts = (shirts)validationContext.ObjectInstance;
            return (shirts.oldPrice == 0 || shirts.newPrice == 0) ? new ValidationResult("Number Should Greater Than 0") : ValidationResult.Success;
        }
    }
}