using EPiServer.Cms.TinyMce.Core;
using EPiServer.Cms.UI.Admin;
using EPiServer.Cms.UI.VisitorGroups;
using EPiServer.ContentApi.Cms.Internal;
using EPiServer.ContentApi.Core.Configuration;
using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.DependencyInjection;
using EPiServer.Framework.Web.Resources;
using EPiServer.Web;
using Geta.NotFoundHandler.Infrastructure.Configuration;
using Geta.NotFoundHandler.Optimizely.Infrastructure.Configuration;
using Geta.Optimizely.Sitemaps;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Perficient.Infrastructure.Display.Extensions;
using Perficient.Infrastructure.Extensions;
using Perficient.Infrastructure.ModelBinders;
using Perficient.Infrastructure.Models.User;
using Perficient.Infrastructure.RenderingServices;
using Perficient.Web.Middleware.Initialization;
using Perficient.Web.Middleware.ViewLocation;
using System;
using System.Linq;

namespace Perficient.Web.Middleware.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static readonly string MediaContainerName = "perficientmedia";
        public static readonly string CorsPolicyName = "_PerficientOrigins";
        /// <summary>
        /// Adds Azure Blob and Event providers
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAzureOptions(this IServiceCollection services)
        {
            services.AddAzureBlobProvider(o =>
            {
                o.ContainerName = MediaContainerName;
            });
            services.AddAzureEventProvider();
        }

        /// <summary>
        /// Enables debugging for Client Resources
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureClientResourceOptions(this IServiceCollection services)
        {
            services.Configure<ClientResourceOptions>(uiOptions =>
            {
                uiOptions.Debug = true;
            });
        }

        /// <summary>
        /// Enables debugging for Client Resources
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicyName,
                    policy =>
                    {
                        policy.WithOrigins("https://prft.local")
                            .AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowAnyMethod();
                    });
            });
        }

        /// <summary>
        /// Configures the Optimizely CMS
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCms(this IServiceCollection services)
        {
            services.AddCms()
               .AddAdmin()
               .AddVisitorGroupsUI()
               .AddCmsAspNetIdentity<SiteUser>();
            services.AddFind();
            services.AddDisplay();
            services.AddImageLoader();
            services.AddEmbeddedLocalization<Startup>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IFirstRequestInitializer), typeof(ContentInstaller)));
        }

        /// <summary>
        /// Adds the Content Delivery API with pre-selected options
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureContentDeliveryApi(this IServiceCollection services, IConfiguration _configuration)
        {
            //services.AddContentDeliveryApi(options =>
            //{
            //    options.SiteDefinitionApiEnabled = true;
            //    options.RequiredRole = "contentapiread";
            //    options.AllowedScopes.Add(string.Empty);
            //}).WithFriendlyUrl();

            
            //// remove null values from serialized data
            services.ConfigureContentDeliveryApiSerializer(settings =>
            {
                settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            //#region replace XhtmlRenderService (Disabled by default)
            //var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(XhtmlRenderService));
            //services.Remove(serviceDescriptor);
            //services.AddTransient(typeof(XhtmlRenderService), typeof(CustomXhtmlRenderService));
            //#endregion
                        
            services.ConfigureContentApiOptions(o =>
            {
                o.EnablePreviewFeatures = true;
                o.ForceAbsolute = true;
                o.FlattenPropertyModel = true;
                o.IncludeInternalContentRoots = true;
                o.IncludeSiteHosts = true;                
                o.MultiSiteFilteringEnabled = true;                
                o.SetValidateTemplateForContentUrl(true);                
            });
            services.AddContentDeliveryApi(); // required, for further configurations, please visit: https://docs.developers.optimizely.com/content-cloud/v1.5.0-content-delivery-api/docs/configuration

            //Adding Content Search API
            services.AddContentSearchApi(o =>
            {
                
            });

            services.AddContentGraph(_configuration);
        }

        /// <summary>
        /// Configures MVC Options
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureMVC(this IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.Conventions.Add(new FeatureConvention());
                o.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
            })
            .AddRazorOptions(ro => ro.ViewLocationExpanders.Add(new FeatureViewLocationExpander()));

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        /// <summary>
        /// Configures Geta Not Found Handler
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureNotFoundHandler(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EPiServerDB");
            services.AddNotFoundHandler(x =>
            {
                x.UseSqlServer(connectionString);
                x.BufferSize = 30;
                x.ThreshHold = 5;
                x.HandlerMode = FileNotFoundMode.On;
                x.IgnoredResourceExtensions = new[] { "jpg", "gif", "png", "css", "ico", "swf", "woff", "css", "js" };
                x.Logging = LoggerMode.On;
                x.LogWithHostname = true;
            });
            services.AddOptimizelyNotFoundHandler();
        }

        /// <summary>
        /// Adds Ping Authentication
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApplicationCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/util/Login";
            });
        }

        /// <summary>
        /// Configures Geta Sitemaps
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSiteMap(this IServiceCollection services)
        {
            services.AddSitemaps(x =>
            {
                x.EnableRealtimeSitemap = false;
                x.EnableRealtimeCaching = true;
                x.EnableLanguageDropDownInAdmin = true;
            });
        }

        /// <summary>
        /// Customizes the Tiny Mce rich text editor
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureTinyMce(this IServiceCollection services)
        {
            services.Configure<TinyMceConfiguration>(config =>
            {
                config.Default()
                    .ContentCss("/ClientResources/styles/tinyMce.css")
                    .Width(750)
                    .Height(450)
                    .AddPlugin("media wordcount anchor code table")
                    .Toolbar("formatselect | styleselect | epi-personalized-content | removeformat | code | fullscreen | epi-link anchor | image epi-image-editor media |  numlist bullist indent outdent bold italic underline alignleft aligncenter alignright alignjustify | table | epi-dnd-processor | removeformat | forecolor backcolor | icons")
                    .AddSetting("image_caption", true)
                    .AddSetting("image_advtab", true)
                    .AddSetting("textcolor_map", new object[] {
                        "512D6D", "Primary",
                        "630840", "Secondary",
                        "BE4D00", "Tertiary",
                        "ED7002", "Tertiary Light",
                        "004159", "Tertiary 2",
                        "C7202F", "Accent",
                        "545859", "Dark Grey",
                        "7E8282", "Medium Grey",
                        "F8F6F6", "Light Grey",
                        "000000", "Black",
                        "FFFFFF", "White",
                        "FDB715", "Alert",
                    })
                    .StyleFormats(new
                    {
                        title = "Buttons",
                        items = new[]
                        {
                            new { title ="Primary", selector = "a", classes = "score-button primary"},
                            new { title ="Secondary", selector = "a", classes = "score-button secondary"},
                            new { title ="Primary Over Dark", selector = "a", classes = "score-button primary over-dark"},
                            new { title ="Secondary Over Dark", selector = "a", classes = "score-button secondary over-dark"},
                            new { title ="Text Primary", selector = "a", classes = "score-button text-primary"},
                            new { title ="Text Over Dark", selector = "a", classes = "score-button text-primary over-dark"}

                        }
                    }, new
                    {
                        title = "Links",
                    }, new
                    {
                        title = "Tables",
                        items = new[]
                        {
                            new {title="Primary", selector = "table", classes = "table-responsive default"},
                            new {title= "Secondary", selector="table", classes = "table-responsive primary"},
                            new {title="Striped", selector="table", classes="table-responsive primary alternate-row-colors"}
                        }
                    });

            });
        }

        /// <summary>
        /// Adds a session that is needed by Wangkani Detection in order to work.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureWangkanaiDetection(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
    }
}
