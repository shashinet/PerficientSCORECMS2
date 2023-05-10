using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.SelectionFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perficient.Infrastructure.EditorDescriptors.Fields
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "FlipCardDirectionEditor")]
    public  class FlipCardDirectionEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(FlipCardDirectionSelectionFactory);
            ClientEditingClass = "epi-cms/contentediting/editors/SelectionEditor";
            base.ModifyMetadata(metadata, attributes);
        }
    }
}
