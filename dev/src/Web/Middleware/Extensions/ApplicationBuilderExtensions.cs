using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Net.Http;



namespace Perficient.Web.Middleware.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapContent();
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }

        /// <summary>
        /// Configures exception handling 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public static void ConfigureExceptionHandling(
            this IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("In Development.");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error/500");
            }
        }

        /// <summary>
        /// Sets up Polly for retry logic for Reverse Proxy
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigurePolly(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                const int retries = 3;
                var logger = ctx.RequestServices.GetRequiredService<ILogger<Startup>>();

                await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(
                    retries,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, duration, retryAttempt, context) => logger.LogInformation($"Retry {retryAttempt}/{retries}"))
                .ExecuteAsync(async () => await next());
            });
        }
    }
}
