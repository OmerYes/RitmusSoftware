using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ritmus.Web.Models.VM
{
    public class ChartVM
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int ProductPrice { get; set; }
        public int TotalPrice { get; set; }
       
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImgURL { get; set; }
        
    }
}