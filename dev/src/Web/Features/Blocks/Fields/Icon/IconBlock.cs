using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.IconLibrary;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Fields.Icon
{
    [ContentType(
      GroupName = GroupNames.Content,
      DisplayName = "Icon Selector",
      GUID = "6c84c447-6494-459c-aeef-b979d2fb44a1",
      Description = "Icon selector component",
      AvailableInEditMode = false)]
    [ImageUrl("~/icons/score/epi_score128_content.png")]
    public class IconBlock : BaseBlock, IPageContentBlock
    {
        [EditorDescriptor(EditorDescriptorType = typeof(IconLibraryEditorDescriptor))]
        [UIHint("IconLibraryEditor")]
        public virtual string Icon { get; set; }
    }
}
