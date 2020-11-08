using Ritmus.Data.Context;
using Ritmus.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmus.Business.Manager.Repository
{
   public class UnitOfWork:IDisposable
    {
        private RitmusContext ritmusContext = new RitmusContext();
        public UnitOfWork()
        {
            ritmusContext = new RitmusContext();
        }
        public UnitOfWork(RitmusContext db)
        {
            ritmusContext = db;
        }
        private GenericRepository<Product> productRepo;
        private GenericRepository<Chart> chartRepo;

        public GenericRepository<Product> ProductRepo
        {
            get
            {
                if (this.productRepo == null)
                {
                    this.productRepo = new GenericRepository<Product>(ritmusContext);
                }
                return productRepo;
            }
        }
        public GenericRepository<Chart> ChartRepo
        {
            get
            {
                if (this.chartRepo == null)
                {
                    this.chartRepo = new GenericRepository<Chart>(ritmusContext);
                }
                return chartRepo;
            }
        }

        public void Save()
        {
            ritmusContext.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ritmusContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
