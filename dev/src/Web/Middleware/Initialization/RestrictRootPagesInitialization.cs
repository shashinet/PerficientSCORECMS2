using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;

using Perficient.Infrastructure.Models.Content;
using Perficient.Infrastructure.Settings.Models.Content;
using Perficient.Infrastructure.Templates.Models;
using Perficient.Web.Features.Articles;
using Perficient.Web.Features.Pages.Home;

namespace Perficient.Web.Middleware.Initialization
{
    [ModuleDependency(typeof(InitializationModule))]
    public class RestrictRootPagesInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var contentTypeRepository = context.Locate.Advanced.GetInstance<IContentTypeRepository>();
            var availableSettingsRepository = context.Locate.Advanced.GetInstance<IAvailableSettingsRepository>();

            var sysRoot = contentTypeRepository.Load("SysRoot") as PageType;
            var startPage = contentTypeRepository.Load<HomePage>();            
            var containerPage = contentTypeRepository.Load<FolderPage>();
            var settingsFolder = contentTypeRepository.Load<SettingsFolder>();            
            var templatesRootFolder = contentTypeRepository.Load<TemplatesRootFolder>();

            var setting = new AvailableSetting { Availability = Availability.Specific };
            setting.AllowedContentTypeNames.Add(startPage.Name);            
            setting.AllowedContentTypeNames.Add(containerPage.Name);
            setting.AllowedContentTypeNames.Add(settingsFolder.Name);            
            setting.AllowedContentTypeNames.Add(templatesRootFolder.Name);

            availableSettingsRepository.RegisterSetting(sysRoot, setting);
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}

