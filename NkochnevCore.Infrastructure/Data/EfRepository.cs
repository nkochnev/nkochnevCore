using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NkochnevCore.Infrastructure.Data
{
    /// <summary>
    ///     Entity Framework repository
    /// </summary>
    public class EfRepository<T> : IRepository<T> where T : BaseDomain
    {
        private readonly IDbContext _context;
        private DbSet<T> _entities;

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            _context = context;
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public virtual IQueryable<T> Table => Entities;

        public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);

            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);

            _context.SaveChanges();
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);

            _context.SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);

            _context.SaveChanges();
        }
    }
}