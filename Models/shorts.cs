using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class shorts
    {
        [Required]
        public int id { get; set; }
        public String photo { get; set; }
        [Required][noZeroShorts]
        public float oldPrice { get; set; }
        [Required][noZeroShorts]
        public float newPrice { get; set; }
        [Required]
        public String description { get; set; }
        public Boolean AddToCart { get; set; }
    }
}