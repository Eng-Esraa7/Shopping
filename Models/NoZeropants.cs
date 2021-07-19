using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class NoZeropants:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var p = (pants)validationContext.ObjectInstance;
            return (p.oldPrice == 0 || p.newPrice == 0) ? new ValidationResult("Number Should Greater Than 0") : ValidationResult.Success;
        }
    }
}