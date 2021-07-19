using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class ViewModelCategory
    {
        public Dresses dress { get; set; }
        public shorts shorts { get; set; }
        public suits suits { get; set; }
        public pants pants { get; set; }
        public shirts shirts { get; set; }
        public children children { get; set; }
        public shoes shoes { get; set; }
        public accessories accessories { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}