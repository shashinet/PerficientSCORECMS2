using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perficient.Web.Middleware.Datalayer
{
    public class EFDataRepository<T> where T : class, IEntityData
    {
        private DbSet<T> _dbSet = null;
        private DbContext _dbContext = null;
        public EFDataRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
        }

        public EFDataRepository<T> Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return this;
        }

        public EFDataRepository<T> AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _dbContext.SaveChanges();
            return this;
        }

        public EFDataRepository<T> Update(T entity)
        {
            var existing = _dbSet.Find(entity.Id);
            if (existing == null)
            {
                return this;
            }
            _dbContext.Entry(existing).CurrentValues.SetValues(entity);
            return this;
        }

        public EFDataRepository<T> Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Remove(entity);
            return this;
        }

        public EFDataRepository<T> Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                return Delete(entity);
            }
            return this;
        }

        public EFDataRepository<T> DeleteRange(IEnumerable<T> entities)
        {
            if (entities?.FirstOrDefault() == null)
            {
                throw new ArgumentOutOfRangeException(nameof(entities));
            }

            _dbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
            return this;
        }

        public EFDataRepository<T> DeleteAll()
        {
            _dbSet.RemoveRange(_dbSet);
            _dbContext.SaveChanges();
            return this;
        }

        public EFDataRepository<T> SaveChanges()
        {
            _dbContext.SaveChanges();
            return this;
        }
    }
}
