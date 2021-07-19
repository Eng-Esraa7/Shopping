using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class accessories
    {
        [Required]
        public int id { get; set; }
        public String photo { get; set; }
        [Required][noZeroAcess]
        public float oldPrice { get; set; }
        [Required][noZeroAcess]
        public float newPrice { get; set; }
        [Required]
        public String description { get; set; }
        public Boolean Bag { get; set; }
        public Boolean Access { get; set; }
        public Boolean Watch { get; set; }
        public Boolean AddToCart { get; set; }

    }
}