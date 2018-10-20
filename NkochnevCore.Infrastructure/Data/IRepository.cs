using System.Collections.Generic;
using System.Linq;

namespace NkochnevCore.Infrastructure.Data
{
    /// <summary>
    ///     Repository
    /// </summary>
    public interface IRepository<T> where T : BaseDomain
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void InsertRange(IEnumerable<T> entity);
        void UpdateRange(IEnumerable<T> entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}