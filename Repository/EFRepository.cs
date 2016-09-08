using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace Repository
{
    abstract public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext context;

        public EFRepository()
        {
            context = new DatabaseEntities();
        }

        public void Delete(TEntity entityToDelete)
        {
            context.Set<TEntity>().Remove(entityToDelete);
            context.Entry(entityToDelete).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = GetById(id);
            if (entityToDelete == null) throw new ArgumentException(String.Format("No data with id: {0}", id));
            Delete(entityToDelete);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IQueryable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return GetAll().Where(filter);
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        abstract public TEntity GetById(int id);
        

        virtual public void Insert(TEntity newentity)
        {
            context.Set<TEntity>().Add(newentity);
            context.Entry(newentity).State = EntityState.Added;
            context.SaveChanges();
        }
    }
}
