using Microsoft.EntityFrameworkCore;

namespace NkochnevCore.Infrastructure.Data
{
    public interface IDbContext
    {
        /// <summary>
        ///     Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseDomain;

        /// <summary>
        ///     Save changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}