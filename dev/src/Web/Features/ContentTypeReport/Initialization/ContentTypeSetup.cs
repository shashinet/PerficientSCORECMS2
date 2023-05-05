using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Perficient.Web.Features.ContentTypeReport.Models;

namespace Perficient.Web.Features.ContentTypeReport.Initialization
{
    public static class ContentTypeReportSetup
    {
        public static IServiceCollection EnableContentTypeReport(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContentTypeDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("EPiServerDB")), ServiceLifetime.Transient);
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<ContentTypeDBContext>();
            dbContext.Database.Migrate();
            return services;
        }

    }
}
