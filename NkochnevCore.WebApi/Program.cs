using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace NkochnevCore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
	        try
	        {
		        logger.Debug("init main");
		        BuildWebHost(args).Run();
	        }
	        catch (Exception ex)
	        {
		        //NLog: catch setup errors
		        logger.Error(ex, "Stopped program because of exception");
		        throw;
	        }
		}

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
	            .ConfigureLogging(logging =>
	            {
		            logging.ClearProviders();
		            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
	            })
	            .UseNLog()  // NLog: setup NLog for Dependency injection
				.Build();
    }
}
