using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ritmus.Business.Manager.Repository
{
    public interface IRepository<T>
    {
        T Add(T entity);
        List<T> GetAll();
        T FirstOrDefault(Expression<Func<T, bool>> exp);
        void Update(T entity);
    }
}
