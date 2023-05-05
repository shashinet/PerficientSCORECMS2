using Perficient.Infrastructure.DynamicProperties.Abstracts;
using Perficient.Infrastructure.DynamicProperties.Models;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Enums;
using Perficient.Web.Features.Pages.GenericLanding;
using System;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.DynamicProperties
{
    public class SideNavigationBlockDynamicProperties : DynamicPropertiesRegistration
    {
        public override Type ForType { get; set; } = typeof(SideNavigationBlock);

        public override Dictionary<Type, string[]> OtherRegisteredTypes { get; set; } = new Dictionary<Type, string[]>
        {
            {typeof(GenericLandingPage), new string[] {nameof(GenericLandingPage.SideNavigation) } },
        };

        public override void RegisterDynamicProperties()
        {
            var sideNavigationTypeDynamicProperty = new DynamicPropertyRegistratorModel(nameof(SideNavigationBlock.NavigationType));

            // Content Area
            sideNavigationTypeDynamicProperty.HideFields.Add(((int)SideNavigationType.ContentArea).ToString(), new string[]
            {
                nameof(SideNavigationBlock.TopLevelLinks),
                nameof(SideNavigationBlock.MenuDirection)
            });
            sideNavigationTypeDynamicProperty.ShowFields.Add(((int)SideNavigationType.ContentArea).ToString(), new string[]
            {
                nameof(SideNavigationBlock.NavigationItems)
            });

            // Top Level Links
            sideNavigationTypeDynamicProperty.HideFields.Add(((int)SideNavigationType.TopLevelLinks).ToString(), new string[]
            {
                nameof(SideNavigationBlock.NavigationItems),
                nameof(SideNavigationBlock.MenuDirection)
            });
            sideNavigationTypeDynamicProperty.ShowFields.Add(((int)SideNavigationType.TopLevelLinks).ToString(), new string[]
            {
                nameof(SideNavigationBlock.TopLevelLinks)
            });

            // Current Content
            sideNavigationTypeDynamicProperty.HideFields.Add(((int)SideNavigationType.CurrentContent).ToString(), new string[]
            {
                nameof(SideNavigationBlock.NavigationItems),
                nameof(SideNavigationBlock.TopLevelLinks)
            });
            sideNavigationTypeDynamicProperty.ShowFields.Add(((int)SideNavigationType.CurrentContent).ToString(), new string[]
            {
                nameof(SideNavigationBlock.MenuDirection)
            });

            DynamicProperties.Add(sideNavigationTypeDynamicProperty);
        }
    }
}
