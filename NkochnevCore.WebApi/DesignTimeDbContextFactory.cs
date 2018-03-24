using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NkochnevCore.Infrastructure.Data;

namespace NkochnevCore.WebApi
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<NkochnevDataContext>
	{
		public NkochnevDataContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var builder = new DbContextOptionsBuilder<NkochnevDataContext>();
			var connectionString = configuration.GetConnectionString("NkochnevDataContext");

			//MS SQL
			//builder.UseSqlServer(connectionString, x => x.MigrationsAssembly("NkochnevCore.Infrastructure"));
			
			//Postgres SQL
			builder.UseNpgsql(connectionString, x => x.MigrationsAssembly("NkochnevCore.Infrastructure"));

			return new NkochnevDataContext(builder.Options);
		}
	}
}
