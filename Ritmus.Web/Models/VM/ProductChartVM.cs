using Ritmus.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ritmus.Web.Models.VM
{
    public class ProductChartVM
    {
      public List <ProductVM> products { get; set; }
      public List<ChartVM> charts { get; set; }
      public int ChartPrice { get; set; }
    }
}