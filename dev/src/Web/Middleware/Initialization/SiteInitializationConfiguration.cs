using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Perficient.Infrastructure.Display;
using Perficient.Infrastructure.Interfaces.Services;
using Perficient.Infrastructure.ModelBinders;
using Perficient.Infrastructure.Services;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Services;
using Perficient.Web.Features.Articles.Repositories;
using Perficient.Web.Features.Blocks.Fields.ResponsivePicture;
using Perficient.Web.Features.Blocks.Fields.SideNavigation;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Interfaces;
using Perficient.Web.Features.ContentTypeReport;
using Perficient.Web.Features.Navigation.Services;
using Perficient.Web.Middleware.ContentMapping;
using Perficient.Web.Middleware.ViewTemplateModelRegistration;

namespace Perficient.Web.Middleware.Initialization
{
    //These resources need to be loaded first

    [ModuleDependency(typeof(InitializationModule))]
    public class SiteInitializationConfiguration : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IsInEditModeAccessor>(locator => () => locator.GetInstance<IContextModeResolver>().CurrentMode.EditOrPreview());
            context.Services.AddSingleton<ServiceAccessor<IContentRouteHelper>>(locator => locator.GetInstance<IContentRouteHelper>);
            context.Services.AddTransient<IModelBinderProvider, DecimalModelBinderProvider>();
            context.Services.AddTransient<ICookieService, CookieService>();
            context.Services.AddSingleton<ISettingsService, SettingsService>();
            context.Services.AddSingleton<INavigationService, NavigationService>();
            context.Services.AddSingleton<IContentMapper, ContentMapper>();
            context.Services.AddTransient<IScorePictureFieldService, ScorePictureFieldService>();
            context.Services.AddTransient<ContentAreaRenderer, CustomContentAreaRenderer>();
            context.Services.AddTransient<IViewTemplateModelRegistrator, ViewTemplateModelRegistrator>();
            context.Services.AddTransient<ISideNavigationBlockService, SideNavigationBlockService>();
            context.Services.AddSingleton<IFeatureExperimentationService, FeatureExperimentationService>();
            context.Services.AddSingleton<IStyleSettingsImportService, StyleSettingsImportService>();
            context.Services.AddSingleton<IContentTypeReportService, ContentTypeReportService>();
            context.Services.AddSingleton<IArticleRepository, ArticleRepository>();            
        }

        public void Initialize(InitializationEngine context)
        {
            
        }

        public void Uninitialize(InitializationEngine context)
        {

        }
    }
}

