using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ritmus.Web.Models.VM
{
    public class ProductVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string ImageURL { get; set; }
    }
}