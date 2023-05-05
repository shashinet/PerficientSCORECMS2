using EPiServer;
using EPiServer.Cms.Shell;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using Perficient.Infrastructure.Extensions;
using Perficient.Infrastructure.Templates.Attributes;
using Perficient.Infrastructure.Templates.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.ServiceLocation;

namespace Perficient.Infrastructure.Templates.Extensions
{
    public static class TemplateExtensions
    {
        /// <summary>
        /// Convert from a ContentReference to a ContentAreaItem
        /// </summary>
        /// <param name="itemRef"></param>
        /// <param name="renderSettings"></param>
        /// <returns></returns>
        private static ContentAreaItem ConvertToContentAreaItem(this ContentReference itemRef, IDictionary<string, object> renderSettings)
        {
            var result = new ContentAreaItem()
            {
                ContentLink = itemRef,
                RenderSettings = renderSettings
            };
            result.LoadDisplayOption();
            return result;
        }

        /// <summary>
        /// Create a new ContentArea of ITemplateContent items based on the source ContentArea
        /// </summary>
        /// <param name="sourceContentAreaItems"></param>
        /// <param name="parentFolder"></param>
        /// <param name="currentDepth"></param>
        /// <param name="maxDepth"></param>
        /// <param name="contentRepository"></param>
        /// <param name="contentAssetHelper"></param>
        /// <returns></returns>
        private static ContentArea CreateContentAreaRecursively(this IEnumerable<ContentAreaItem> sourceContentAreaItems, ContentReference parentFolder, int currentDepth, int maxDepth, IContentRepository contentRepository, ContentAssetHelper contentAssetHelper)
        {
            var sourceContentList = sourceContentAreaItems?.GetContentItems<ITemplateContent>();
            if (currentDepth > maxDepth)
            {
                return null;
            }

            if (sourceContentList?.Any() != true)
            {
                return null;
            }

            var contentArea = new ContentArea();

            // from each ITemplateContent, try to create a new instance of it
            foreach (var sourceContentItem in sourceContentList)
            {
                var iSourceContentItem = sourceContentItem as IContent;

                // create a new instance
                var newContent = contentRepository.GetDefault<IContent>(parentFolder, iSourceContentItem.ContentTypeID);
                newContent.Name = iSourceContentItem.Name;
                var newContentRef = contentRepository.Save(newContent, SaveAction.SkipValidation, AccessLevel.NoAccess);
                var newContentClone = contentRepository.Get<ContentData>(newContentRef).CreateWritableClone();

                sourceContentItem.PopulateContentTo(newContentClone as IContent, currentDepth, maxDepth, contentRepository, contentAssetHelper);

                // save the new instance
                contentRepository.Save(newContentClone as IContent, SaveAction.Publish, AccessLevel.NoAccess);

                // add the new instance to the ContentArea
                var contentAreaItem = newContentRef.ConvertToContentAreaItem(sourceContentAreaItems.FirstOrDefault(i => i.ContentLink.ID == iSourceContentItem.ContentLink.ID)?.RenderSettings);
                contentArea.Items.Add(contentAreaItem);
            }
            return contentArea;
        }

        /// <summary>
        /// Populate data from source content to an existing targetContent
        /// </summary>
        /// <param name="sourceContent"></param>
        /// <param name="targetContent"></param>
        /// <param name="currentDepth"></param>
        /// <param name="maxDepth"></param>
        /// <param name="contentRepository"></param>
        /// <param name="contentAssetHelper"></param>
        public static void PopulateContentTo(this ITemplateContent sourceContent, IContent targetContent, int currentDepth, int maxDepth, IContentRepository contentRepository, ContentAssetHelper contentAssetHelper)
        {
            if (ContentReference.IsNullOrEmpty(targetContent.ContentLink))
            {
                return;
            }

            // create local asset folder for target content ('For This Block' or 'For This Page' folder)
            var localAssetFolder = contentAssetHelper.GetOrCreateAssetFolder(targetContent.ContentLink);

            foreach (var property in sourceContent.Property)
            {
                // only copy property data and ignore meta data
                if (IgnoredProperties.Contains(property.Name) || property.IsMetaData)
                {
                    continue;
                }

                if (targetContent.Property[property.Name] != null)
                {
                    // check if the current property is decorated with TemplatesIgnorePropertyAttribute
                    var ignorePropAttr = sourceContent.GetOriginalType().GetProperty(property.Name)?.GetCustomAttribute(typeof(TemplatesIgnorePropertyAttribute)) as TemplatesIgnorePropertyAttribute;
                    if (ignorePropAttr != null && ignorePropAttr.IgnoreProperty)
                    {
                        continue;
                    }

                    // check if current property is a ContentArea
                    if (property.Value is ContentArea sourceContentArea)
                    {
                        // populate a new content area of only ITemplateContent items based on the source content area
                        var newContentArea = sourceContentArea?.FilteredItems?.CreateContentAreaRecursively(localAssetFolder.ContentLink, currentDepth + 1, maxDepth, contentRepository, contentAssetHelper);

                        // set the newly created content area to the target content property
                        targetContent.SetPropertyValue(property.Name, newContentArea);
                    }
                    else
                    {
                        // else just normally copy the data
                        targetContent.SetPropertyValue(property.Name, property.Value);
                    }
                }
            }
        }

        public static readonly IEnumerable<string> IgnoredProperties = new[]
        {
            "SelectedTemplate",
            "OldTemplate"
        };
    }
}
