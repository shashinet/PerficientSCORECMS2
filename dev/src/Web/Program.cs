using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Perficient.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                    .AddJsonFile("appsettings.local.json", true, true)
                    // might remove this in favour of the local.json only, since it's one name to remember
                    // and we don't need to commit local JSON files
                    .AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true)
                    .AddKeyPerFile(Path.Combine(Directory.GetCurrentDirectory(), "StyleSettingsImport"), true, true)
                    .AddEnvironmentVariables();
            IConfiguration Configuration = configBuilder.Build();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = (environment == Environments.Development || Configuration.GetValue<bool>("RunAsDevelopment"));
            var logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            Log.Logger = logger;

            Log.Information($"Starting application in environment {environment}.");
            Log.Information($"Loading Configuration from JSON files: {string.Join(", ", configBuilder.Sources.Where(s => s is FileConfigurationSource).Select(s => (s as FileConfigurationSource).Path))}");

            try
            {
                var loggerFactory = (Microsoft.Extensions.Logging.ILoggerFactory)new LoggerFactory();
                loggerFactory.AddSerilog(logger);
                CreateHostBuilder(args, Configuration, isDevelopment, loggerFactory.CreateLogger<Startup>()).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error("Begin Startup Failed Message");
                Log.Fatal(ex, "End Startup Failed Message");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration, bool isDevelopment, ILogger<Startup> logger)
        {
            Log.Information("Create Host Builder");
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration(builder => builder.AddConfiguration(configuration));

            if (isDevelopment)
            {
                Log.Information("Using Development host builder configuration");
                // dev specific configuration can be added here, like local logging needs
                // or Dev specific utilities
            }            

            return hostBuilder
                .ConfigureCmsDefaults()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup( context => new Startup(context.HostingEnvironment,configuration,logger,isDevelopment));
                });
        }
    }
}
