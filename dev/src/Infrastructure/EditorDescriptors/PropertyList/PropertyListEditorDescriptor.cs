using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;

namespace Perficient.Infrastructure.EditorDescriptors.PropertyList
{
    public class PropertyListEditorDescriptor<T> : CollectionEditorDescriptor<T> where T : new()
    {
        public PropertyListEditorDescriptor()
        {
            ClientEditingClass = "custom-scripts/editors/propertyListCollectionEditor";
        }
    }
}
