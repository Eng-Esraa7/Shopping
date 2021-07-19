using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Shopping.Models
{
    public class suits
    {
        [Required]
        public int id { get; set; }
        public String photo { get; set; }
        [Required][noZeroSuits]
        public float oldPrice { get; set; }
        [Required][noZeroSuits]
        public float newPrice { get; set; }
        [Required]
        public String description { get; set; }
        public Boolean AddToCart { get; set; }
    }
}