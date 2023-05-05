using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Skia;

namespace Perficient.Infrastructure.Extensions
{
    public static class ImageLoaderExtension
    {
        public static void AddImageLoader(this IServiceCollection services)
        {
            services.AddSingleton<IImageLoadingService, SkiaImageLoadingService>();
        }
    }
}
