using EPiServer;
using EPiServer.ServiceLocation;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Enums;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MaxNestingDepthAttribute : ValidationAttribute
    {
        private readonly Injected<IContentLoader> _contentLoader;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            var contentLoaderService = _contentLoader.Service;

            if (value is SideNavigationBlock sideNavBlock)
            {
                return ValidateSideNavigationBlock(sideNavBlock, contentLoaderService);
            }

            return null;
        }

        #region Private Methods

        private static ValidationResult ValidateSideNavigationBlock(SideNavigationBlock currentBlock, IContentLoader contentLoader)
        {
            if (currentBlock.NavigationItems?.Count == 0
                || currentBlock.NavigationType != SideNavigationType.ContentArea)
            {
                return ValidationResult.Success;
            }

            var maxDepth = currentBlock.NavigationMaxDepth;
            var currentDepth = 1;

            // Grab link items that have their own child link items
            var nextDepthLinks = currentBlock.NavigationItems?.Items?
                .Select(x => contentLoader.Get<SideNavigationLinkItem>(x.ContentLink))
                ?.Where(x => x.NavItemChildLinks?.Count > 0);

            // Loop until no link items have any child link items
            while (nextDepthLinks != null && nextDepthLinks.Any())
            {
                currentDepth++;

                if (currentDepth > maxDepth)
                {
                    return new ValidationResult($"Max nesting depth of {maxDepth} reached. Increase the Navigation Max Depth value to add more items.");
                }

                nextDepthLinks = nextDepthLinks
                    .SelectMany(linkItem => linkItem.NavItemChildLinks?.Items)
                    ?.Select(contentAreaItem => contentLoader.Get<SideNavigationLinkItem>(contentAreaItem.ContentLink))
                    ?.Where(nextDepthLinkItems => nextDepthLinkItems.NavItemChildLinks?.Count > 0);
            }

            return ValidationResult.Success;
        }

        #endregion
    }
}
