using EPiServer.Cms.Shell.UI.UIDescriptors.ViewConfigurations.Internal;
using System;

namespace Perficient.Infrastructure.DynamicProperties.EditorDescriptors
{
    public class DynamicPropertyEditorDescriptor : FormEditing
    {
        public DynamicPropertyEditorDescriptor(Type desiredType)
        {
            Key = "extended-dynamicpropertiesview";
            ForType = desiredType;
            ViewType = "score/editors/dynamicPropertiesEditor";
        }
    }
}
