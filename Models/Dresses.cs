using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Shopping.Models
{
    public class Dresses
    {
        [Required]
        public int id { get; set; }
        public string photo { get; set; }
        [nozero][Required]
        public float? oldPrice { get; set; }
        [Required][nozero]
        public float? newPrice { get; set; }
        [Required]
        public String description { get; set; }
        public Boolean AddToCart { get; set; }
    }
}