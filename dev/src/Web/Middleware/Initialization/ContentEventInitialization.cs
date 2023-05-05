using EPiServer.Core;
using EPiServer;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Perficient.Infrastructure.Interfaces.Content;

namespace Perficient.Web.Middleware.Initialization
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ContentEventInitialization : IInitializableModule
    {
        private Injected<IContentEvents> _contentEvents;

        public void Initialize(InitializationEngine context)
        {
            _contentEvents.Service.SavingContent += OnSavingContent;
            _contentEvents.Service.CheckingInContent += OnCheckingInContent;
            _contentEvents.Service.PublishingContent += OnPublishingContent;
        }

        public void Uninitialize(InitializationEngine context)
        {
            _contentEvents.Service.SavingContent -= OnSavingContent;
            _contentEvents.Service.CheckingInContent -= OnCheckingInContent;
            _contentEvents.Service.PublishingContent -= OnPublishingContent;
        }

        private void OnSavingContent(object sender, ContentEventArgs e)
        {
            var content = e.Content as IContentSaving;
            if (content == null) { return; }

            content.SavingContent(sender, e);
        }

        private void OnCheckingInContent(object sender, ContentEventArgs e)
        {
            var content = e.Content as IContentCheckingIn;
            if (content == null) { return; }

            content.CheckingInContent(sender, e);
        }

        private void OnCreatingContent(object sender, ContentEventArgs e)
        {
            var content = e.Content as IContentCreating;
            if (content == null) { return; }

            content.CreatingContent(sender, e);
        }

        private void OnPublishingContent(object sender, ContentEventArgs e)
        {
            var content = e.Content as IContentPublishing;
            if (content == null) { return; }

            content.PublishingContent(sender, e);
        }
    }
}
