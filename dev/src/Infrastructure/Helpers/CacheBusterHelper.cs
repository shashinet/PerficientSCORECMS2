using EPiServer;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace Perficient.Infrastructure.Helpers
{
    public class CacheBusterHelper
    {
        private static readonly Lazy<IWebHostEnvironment> _webHhostEnvironment =
           new Lazy<IWebHostEnvironment>(() => ServiceLocator.Current.GetInstance<IWebHostEnvironment>());

        protected CacheBusterHelper() { }

        public static string Version(string rootRelativePath)
        {
            if (rootRelativePath.StartsWith("~"))
            {
                rootRelativePath = rootRelativePath.Substring(1);
            }

            var version = CacheManager.Get(rootRelativePath)?.ToString();
            if (!string.IsNullOrEmpty(version))
            {
                return version;
            }

            version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (version == "1.0.0.0") // default version number
            {
                try
                {


                    var absolutePath = $"{_webHhostEnvironment.Value.WebRootPath}{rootRelativePath.Replace("/", "\\")}";
                    var lastChangedDateTime = File.GetLastWriteTime(absolutePath);
                    version = $"1.0.0.{lastChangedDateTime.Ticks}";
                }
                catch
                {
                    var logger = LogManager.GetLogger(typeof(CacheBusterHelper));

                    // route for getting display options for a block filtered by allowed types
                    logger.Debug($"[CacheBusterHelper]:[Version] - Exception occurred trying to read Ticks from File @ {rootRelativePath}. Defaulting to 1.0.0.0");
                }
            }

            var versionedUrl = rootRelativePath + "?v=" + version;
            CacheManager.Insert(rootRelativePath, versionedUrl);

            return versionedUrl;
        }
    }
}
