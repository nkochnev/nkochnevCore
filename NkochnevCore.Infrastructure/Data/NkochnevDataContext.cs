using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace NkochnevCore.Infrastructure.Data
{
    public class NkochnevDataContext : DbContext, IDbContext
    {
        public NkochnevDataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseDomain
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Interface that all of our Entity maps implement
            var mappingInterface = typeof(IEntityTypeConfiguration<>);

            // Types that do entity mapping
            var mappingTypes = typeof(NkochnevDataContext).GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.GetInterfaces()
                    .Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));

            // Get the generic Entity method of the ModelBuilder type
            var entityMethod = typeof(ModelBuilder).GetMethods()
                .Single(x => x.Name == "Entity" &&
                             x.IsGenericMethod &&
                             x.ReturnType.Name == "EntityTypeBuilder`1");

            foreach (var mappingType in mappingTypes)
            {
                // Get the type of entity to be mapped
                var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();

                // Get the method builder.Entity<TEntity>
                var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

                // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
                var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

                // Create the mapping type and do the mapping
                var mapper = Activator.CreateInstance(mappingType);
                mapper.GetType().GetMethod("Configure").Invoke(mapper, new[] {entityBuilder});
            }
        }
    }
}