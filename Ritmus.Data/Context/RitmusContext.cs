using Ritmus.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmus.Data.Context
{
  public class RitmusContext : DbContext
    {
        public RitmusContext()
        {
            Database.Connection.ConnectionString = "Server=.;Database=Ritmuus_DB;UID=sa;PWD=123456";
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Chart> Charts { get; set; }
        //public DbSet<ProductChart> ProductCharts { get; set; } 
    }
}
