using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmus.Data.Entity
{
   public class Chart:BaseEntity
    {
        public int Quantity { get; set; }
       
        public int TotalPrice { get; set; }
      
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Products { get; set; }

    }
}
