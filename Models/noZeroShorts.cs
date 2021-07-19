using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class noZeroShorts:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var shorts = (shorts)validationContext.ObjectInstance;
            return( shorts.oldPrice == 0 || shorts.newPrice == 0 ) ? new ValidationResult("Number Should Greater Than 0") : ValidationResult.Success;
        }
    }
}