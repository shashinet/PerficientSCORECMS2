using System;
using EPiServer;
using EPiServer.Cms.Shell;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.Configuration;
using Perficient.Infrastructure.Templates.Extensions;
using Perficient.Infrastructure.Templates.Interfaces;

namespace Perficient.Infrastructure.Templates.Initializations
{
    [InitializableModule]
    public class TemplateEventsInitialization : IInitializableModule
    {
        private int _maximumDepth;
        private IContentRepository _contentRepository;
        private ContentAssetHelper _contentAssetHelper;

        public void Initialize(InitializationEngine context)
        {
            _contentRepository = context.Locate.Advanced.GetInstance<IContentRepository>();
            _contentAssetHelper = context.Locate.Advanced.GetInstance<ContentAssetHelper>();

            var configuration = context.Locate.Advanced.GetInstance<IConfiguration>();
            _maximumDepth = int.TryParse(configuration["TemplateSettings:MaximumDepth"], out int maximumDepth) ? maximumDepth : 10;
            context.Locate.Advanced.GetInstance<IContentEvents>().CreatedContent += Instance_CreatedContent;
            context.Locate.Advanced.GetInstance<IContentEvents>().CreatingContent += Instance_CreatingContent;
        }

        private void Instance_CreatingContent(object sender, ContentEventArgs e)
        {
            if (e.Content is not ITemplateContent currentContent
                || ContentReference.IsNullOrEmpty(currentContent.SelectedTemplate))
            {
                return;
            }

            var templateContent = _contentRepository.Get<IContent>(currentContent.SelectedTemplate);
            if (e.Content.ContentTypeID != templateContent.ContentTypeID)
            {
                e.CancelAction = true;
                e.CancelReason = "Template type is mismatched with the current content type";
            }
        }

        private void Instance_CreatedContent(object sender, ContentEventArgs e)
        {
            if (e.Content is not ITemplateContent currentContent
                || ContentReference.IsNullOrEmpty(currentContent.SelectedTemplate)
                || currentContent.SelectedTemplate.ID == currentContent.OldTemplate?.ID)
            {
                return;
            }
            
            // getting the selected template
            if (!_contentRepository.TryGet(currentContent.SelectedTemplate, out ITemplateContent selectedTemplate))
            {
                return;
            }

            // populate the data from selected template to the current content, any nested content will be saved under "For This Page" or "For This Block" folder
            var writableClone = (e.Content as ContentData).CreateWritableClone();
            var writableIContent = writableClone as IContent;
            selectedTemplate.PopulateContentTo(writableIContent, 1, _maximumDepth, _contentRepository, _contentAssetHelper);

            // finally set the OldTemplate value after content has been successfully populated
            writableIContent.SetPropertyValue("OldTemplate", currentContent.SelectedTemplate);

            _contentRepository.Save(writableIContent, SaveAction.Save, AccessLevel.NoAccess);
        }
        

        public void Uninitialize(InitializationEngine context)
        {
            context.Locate.Advanced.GetInstance<IContentEvents>().CreatedContent -= Instance_CreatedContent;
            context.Locate.Advanced.GetInstance<IContentEvents>().CreatingContent -= Instance_CreatingContent;
        }
    }
}
