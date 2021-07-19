using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class shoes
    {
        [Required]
        public int id { get; set; }
        public String photo { get; set; }
        [Required][noZeroShoeses]
        public float oldPrice { get; set; }
        [Required][noZeroShoeses]
        public float newPrice { get; set; }
        public String color { get; set; }
        [Required][noZeroShoeses]
        public int Size { get; set; }
        [Required]
        public String description { get; set; }
        public bool Girls { get; set; }
        [Required]
        public bool Man { get; set; }
        public Boolean AddToCart { get; set; }
    }
}