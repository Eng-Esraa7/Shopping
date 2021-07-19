using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class checkOut
    {
        public int id { get; set; }
        [Required]
        public String UserId { get; set; }
        [Required]
        public String FullName { get; set; }
        public String nameUser { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String Phone { get; set; }
        [Required]
        public float Total { get; set; }
        public Boolean finish { get; set; }
    }
}