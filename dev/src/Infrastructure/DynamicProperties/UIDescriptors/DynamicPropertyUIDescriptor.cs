using System;

using EPiServer.Shell;

namespace Perficient.Infrastructure.DynamicProperties.UIDescriptors
{
    public class DynamicPropertyUIDescriptor : UIDescriptor
    {
        public DynamicPropertyUIDescriptor(Type desiredType) : base(desiredType, ContentTypeCssClassNames.SharedBlock)
        {
            this.DefaultView = "extended-dynamicpropertiesview";
            AddDisabledView(CmsViewNames.AllPropertiesView);
        }
    }
}
