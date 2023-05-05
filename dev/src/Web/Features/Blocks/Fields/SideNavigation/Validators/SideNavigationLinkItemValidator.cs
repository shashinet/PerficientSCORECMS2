using EPiServer;
using EPiServer.Core;
using EPiServer.Validation;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Validators
{
    public class SideNavigationLinkItemValidator : IContentSaveValidate<SideNavigationLinkItem>
    {
        private readonly IContentRepository _contentRepo;

        public SideNavigationLinkItemValidator(IContentRepository contentRepo)
        {
            _contentRepo = contentRepo;
        }

        public IEnumerable<ValidationError> Validate(SideNavigationLinkItem instance, ContentSaveValidationContext context)
        {
            // New link items do not have a content link assigned at this point
            if (context.NewVersionRequired)
            {
                return Enumerable.Empty<ValidationError>();
            }

            var menuRoot = TryGetMenuRoot(instance, out int depth);

            if (menuRoot == null)
            {
                return Enumerable.Empty<ValidationError>();
            }

            var sideNavigationBlockProperty = menuRoot.Property
                .Where(x => x.Value is SideNavigationBlock)
                ?.FirstOrDefault()
                ?.Value as SideNavigationBlock;

            if (sideNavigationBlockProperty == null)
            {
                return Enumerable.Empty<ValidationError>();
            }

            var maxDepth = sideNavigationBlockProperty.NavigationMaxDepth;

            if (depth > maxDepth)
            {
                return new List<ValidationError>()
                {
                    new ValidationError()
                    {
                        ErrorMessage = $"Max nesting depth of {maxDepth} reached. Increase the Navigation Max Depth value to add more items.",
                        Severity = ValidationErrorSeverity.Error
                    }
                };
            }

            return Enumerable.Empty<ValidationError>();
        }

        private IContent TryGetMenuRoot(SideNavigationLinkItem instance, out int currentDepth)
        {
            var nextContent = instance as IContent;

            // Validate new link items by including them in their parent's check
            currentDepth = instance.NavItemChildLinks?.Count > 0
                ? 1
                : 0;

            while (nextContent is SideNavigationLinkItem)
            {
                var referenceToBlock = _contentRepo
                    .GetReferencesToContent(nextContent.ContentLink, false)
                    ?.FirstOrDefault();

                if (referenceToBlock == null)
                {
                    return null;
                }

                nextContent = _contentRepo.Get<IContent>(referenceToBlock.OwnerID);

                if (nextContent == null)
                {
                    return null;
                }

                currentDepth++;
            }

            return nextContent;
        }
    }
}
