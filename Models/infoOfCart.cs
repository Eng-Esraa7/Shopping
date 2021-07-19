using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class infoOfCart
    {
        public int id { get; set; }
        public int categoryId { get; set; }
        public string nameUser { get; set; }
        public String nameCategory { get; set; }
        public String photo { get; set; }
        public float price { get; set; }
        public int noCategory { get; set; }
        public String UserId { get; set; }
        public Boolean check { get; set; }
    }
}
