using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Controllers
{
    public class ContactUs
    {
        public int id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
         [Required]
        public string PhoneNumber { get; set; }
    }
}