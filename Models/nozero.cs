using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class nozero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dresses = (Dresses)validationContext.ObjectInstance;
            return (dresses.oldPrice == 0 || dresses.newPrice == 0) ? new ValidationResult("Number Should Greater Than 0") : ValidationResult.Success;
        }
    }
}