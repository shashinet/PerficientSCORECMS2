using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Alert
{
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Alert Block",
        AvailableInEditMode = true,
        GUID = "7018FE03-C2DA-42A1-A2E4-81F87AD8CCD5",
        Description = "Global Alerts for the Site")]
    public class AlertBlock : BaseBlock, IAlertBlock
    {

        [Display(Name = "Title", Order = 10)]
        public virtual string Title { get; set; }

        [Display(Name = "Alert Text", Order = 20)]
        public virtual XhtmlString MainText { get; set; }

        [CultureSpecific]
        [Display(Name = "CTA Buttons", Order = 30)]
        [AllowedTypes(typeof(ICallToActionBlock))]
        public virtual ContentArea CallToActionContentArea { get; set; }

        [Display(Name = "Allow Closing of Alert", Order = 50)]
        public virtual bool IsClosable { get; set; }

        [Display(Name = "Days to expire close cookie", Order = 80)]

        public virtual int DaysExpire { get; set; }


        [Display(GroupName = TabNames.Styles, Order = 50, Name = "Background Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(ColorPickerEditorDescriptor))]
        [UIHint("ColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string BackgroundColor { get; set; }

        [Display(GroupName = TabNames.Styles, Order = 40, Name = "Text Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(FontColorPickerEditorDescriptor))]
        [UIHint("FontColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string TextColor { get; set; }

        [ScaffoldColumn(false)]
        [Ignore]
        public virtual bool IsAlertDismissed { get; set; }


        public override void SetDefaultValues(ContentType contentType)
        {
            DaysExpire = 7;
        }
    }
}
