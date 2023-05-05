using Microsoft.Extensions.DependencyInjection;
using Perficient.Infrastructure.Display.Resolutions;

namespace Perficient.Infrastructure.Display.Extensions
{
    public static class DisplayExtensions
    {
        public static void AddDisplay(this IServiceCollection services)
        {
            services.AddDisplayResolutions();
        }

        private static void AddDisplayResolutions(this IServiceCollection services)
        {
            services.AddSingleton<StandardResolution>();
            services.AddSingleton<IpadHorizontalResolution>();
            services.AddSingleton<IphoneVerticalResolution>();
            services.AddSingleton<AndroidVerticalResolution>();
        }
    }
}
