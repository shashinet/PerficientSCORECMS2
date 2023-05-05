using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;
using Perficient.Infrastructure.Templates.Services;

namespace Perficient.Infrastructure.Templates.Initializations
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class TemplateBaseInitialization : IConfigurableModule
    {
        private IServiceCollection _services;

        void IConfigurableModule.ConfigureContainer(ServiceConfigurationContext context)
        {
            _services = context.Services;
            _services.AddSingleton<ITemplatesService, TemplatesService>(); // Added as singleton
        }

        void IInitializableModule.Initialize(InitializationEngine context)
        {
            context.InitComplete += delegate
            {
                context.Locate.Advanced.GetInstance<ITemplatesService>().InitializeTemplates();
            };
        }

        void IInitializableModule.Uninitialize(InitializationEngine context)
        {
            // Do nothing
        }
    }
}
