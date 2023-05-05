
using DoubleJay.Epi.EnhancedPropertyList;
using EPiServer.Marketing.Testing.Web.Initializers;
using Geta.NotFoundHandler.Infrastructure.Initialization;
using Geta.Optimizely.Categories.Configuration;
using Geta.Optimizely.Categories.Find.Infrastructure.Initialization;
using Geta.Optimizely.Categories.Infrastructure.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Perficient.Infrastructure.Initialization.Startup;
using Perficient.Web.Features.ContentTypeReport.Initialization;
using Perficient.Web.Middleware.ContentMapping;
using Perficient.Web.Middleware.Extensions;
using Serilog;
using UNRVLD.ODP.VisitorGroups.Initilization;

namespace Perficient.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _configuration;
        private ILogger<Startup> _logger;
        private bool _isDevelopment;

        public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _configuration = configuration;
        }

        public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration, ILogger<Startup>logger, bool isDevelopment = false): this(webHostingEnvironment,configuration)
        {
            _logger = logger;   // logging can now happen in startup
            _isDevelopment = isDevelopment; // flag for dev only or specific extension calls
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information($"Startup - Configuring Services.");
            if (_isDevelopment)
            {
                services.ConfigureClientResourceOptions();
            }
            else
            {
                services.ConfigureAzureOptions();
            }
            

            services.AddApplicationInsightsTelemetry();
            services.ConfigureMVC();
            services.ConfigureCms();
            services.AddEnhancedPropertyList();
            services.ConfigureCors();
            services.ConfigureContentDeliveryApi(_configuration);
            services.AddDetection(); //Add detection services container and device resolver service.           
            services.ConfigureWangkanaiDetection();
            services.ConfigureTinyMce();
            services.ConfigureSiteMap();
            services.AddGetaCategories();
            services.ConfigureNotFoundHandler(_configuration);
            services.AddAutoMapper(typeof(MappingProfile));
            services.ConfigureApplicationCookie();

            services.AddOptiExperiements(_configuration);
            services.EnableContentTypeReport(_configuration);
            services.AddODPVisitorGroups();
            services.AddABTesting(_configuration.GetConnectionString("EPiServerDB"));
            

            // Add Reverse Proxy Service
            //var proxyBuilder = services.AddReverseProxy();
            Log.Information($"Startup - Finished Configuring Services.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger)
        {
            Log.Information($"Startup - Configuring App.");
            var options = new RewriteOptions().AddRedirect("episerver$", "episerver/cms");
            app.UseGetaCategories();
            app.UseGetaCategoriesFind();
            app.UseRewriter(options);

            app.ConfigureExceptionHandling(env, logger);
            app.UseStatusCodePagesWithReExecute("/error/{0}");  //Set up 404 Handler
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(Perficient.Web.Middleware.Extensions.ServiceCollectionExtensions.CorsPolicyName);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNotFoundHandler();
            app.ConfigureEndpoints();
            //app.ConfigurePolly();
            //app.ConfigureReverseProxy(forwarder); Not used in every project. Uncomment if needed
            Log.Information($"Startup - Finished Configuring App.");
        }
    }
}
