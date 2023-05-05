using EPiServer.Logging;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptimizelySDK;
using OptimizelySDK.Config;
using System;
using ILogger = EPiServer.Logging.ILogger;

namespace Perficient.Infrastructure.Initialization.Startup
{

    public class OptiExperimentOptions
    {
        public string Key { get; set; }

        public TimeSpan PollingInterval { get; set; }


    }

    public static class OptiExperimentConfiguration
    {
        private static readonly ILogger _logger = LogManager.GetLogger(typeof(OptiExperimentConfiguration));

        public static void AddOptiExperiements(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddOptions<OptiExperimentOptions>().Bind(_configuration.GetSection("Episerver:Experiments"));

            var serviceProvider = services.BuildServiceProvider();

            var options = serviceProvider.GetRequiredService<IOptions<OptiExperimentOptions>>()?.Value;

            if (options == null)
            {
                _logger.Error(message: "No configuration found for Optimizely Experiments");
            }

            ProjectConfigManager fullStackConfig = new HttpProjectConfigManager.Builder()
                .WithSdkKey(options.Key)
                .WithPollingInterval(options.PollingInterval)
                .Build();

            services.AddSingleton<IOptimizely>(new OptimizelySDK.Optimizely(fullStackConfig));
        }
    }

}
