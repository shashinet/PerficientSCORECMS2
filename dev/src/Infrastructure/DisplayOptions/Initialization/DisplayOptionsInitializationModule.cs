using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Perficient.Infrastructure.DisplayOptions.Loader;

namespace Perficient.Infrastructure.DisplayOptions.Initialization
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DisplayOptionsInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            if (context.HostType == HostType.WebApplication)
            {
                DisplayOptionsLoader.LoadDisplayOptions(ServiceLocator.Current.GetInstance<EPiServer.Web.DisplayOptions>());
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
