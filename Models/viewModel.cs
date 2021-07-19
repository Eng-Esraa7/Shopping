using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class viewModel
    {
        public IEnumerable<infoOfCart> InfoOfCart { get; set; }
        public checkOut CheckOut { get; set; }

    }
}