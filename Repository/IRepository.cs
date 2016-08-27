using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity:class
    {
        void Insert(TEntity newentity);
        void Delete(int id);
        void Delete(TEntity entityToDelete);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

    }
}
