using Ritmus.Data.Context;
using Ritmus.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ritmus.Business.Manager.Repository
{
   public class GenericRepository<TEntity>:IRepository<TEntity> where TEntity:BaseEntity
    {
        internal RitmusContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(RitmusContext context)
        {
            this.Context = context;
            this.DbSet = Context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            if (entity != null)
            {
                entity.IsDeleted = false;
                DbSet.Add(entity);
                Context.SaveChanges();
            }
            return entity;
        }
        public List<TEntity> GetAll()
        {
            return DbSet.Where(q => q.IsDeleted==false ).ToList();
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> exp)
        {
            return DbSet.Where(q => q.IsDeleted == false ).FirstOrDefault(exp);
        }
        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                var _entity = DbSet.Find(entity.ID);
                Context.Entry(_entity).CurrentValues.SetValues(entity);
                Context.SaveChanges();
            }
        }
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
