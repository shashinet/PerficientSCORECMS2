using System;

using EPiServer.Logging;

using HtmlAgilityPack;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;

namespace Perficient.Infrastructure.Extensions
{
    public static class XhtmlStringExtensions
    {
        // Read this only once for performance
        private static readonly bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;

        /// <summary>
        /// Parses the XHtml String and forces all relative images to be absolute
        /// </summary>
        public static string ForceAbsoluteImgUrls(this string input)
        {
            if (IsDevelopment)
            {
                return input;
            }

            var logger = LogManager.GetLogger(typeof(XhtmlStringExtensions));
            var logPrefix = $"{nameof(XhtmlStringExtensions)}->ForceAbsoluteImgUrls:";

            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(input);

                // find all image tags, if there are none just return out- otherwise we need to adjust them

                var imgs = doc.DocumentNode.SelectNodes("//img");
                if (imgs == null)
                    return input;

                var scheme = EPiServer.Web.SiteDefinition.Current.SiteUrl.Scheme;
                var serverUrl = new HostString(EPiServer.Web.SiteDefinition.Current.SiteUrl.Host);

                logger.Information($"{logPrefix} scheme={scheme} & serverUrl={serverUrl}");

                // for each image, interrogate src accordingly
                foreach (var img in imgs)
                {
                    var src = img.Attributes["src"]?.Value;

                    logger.Information($"{logPrefix} img src found: {src}");

                    // try parsing uri and determine if it's absolute
                    Uri uri = null;
                    if (Uri.TryCreate(src, UriKind.Absolute, out uri))
                    {
                        if (uri.IsAbsoluteUri)
                        {
                            logger.Information($"{logPrefix} img src is absolute, skipping ({src})");
                            continue; // if it's already absolute, just continue processing
                        }

                    }

                    // must not be absolute, so go ahead and build an absolute url for it
                    var newSrc = UriHelper.BuildAbsolute(scheme, serverUrl, src);
                    logger.Information($"{logPrefix} img src is not absolute, fixing setting old {src} to {newSrc}");
                    img.SetAttributeValue("src", newSrc);
                }

                var outerHtml = doc.DocumentNode.OuterHtml;
                return outerHtml; // return out the new resulting HTML
            }
            catch (Exception ex)
            {
                logger.Error($"{logPrefix} Error encountered when trying to force absolute URLs in XhtmlStrings", ex);
            }

            return input;
        }
    }
}