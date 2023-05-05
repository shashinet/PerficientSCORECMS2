using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Perficient.Infrastructure.Interfaces.Services;

namespace Perficient.Infrastructure.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]    
    
    public class StyleSettingsInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var styleSettingsImportService = ServiceLocator.Current.GetInstance<IStyleSettingsImportService>();
            styleSettingsImportService.UpdateStyleSettings();
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
