using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NkochnevCore.Infrastructure;
using NkochnevCore.Infrastructure.Data;

namespace NkochnevCore.WebApi
{
    public class WithMigrationsDbContextFactory : IDesignTimeDbContextFactory<NkochnevDataContext>
    {
        public NkochnevDataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<NkochnevDataContext>();
            var connectionString = configuration.GetConnectionString("NkochnevDataContext");

            builder.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(AssemblyAnchor).Assembly.FullName));

            return new NkochnevDataContext(builder.Options);
        }
    }
}