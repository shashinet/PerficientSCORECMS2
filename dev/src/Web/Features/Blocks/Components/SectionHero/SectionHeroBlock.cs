using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Components.Vimeo;
using Perficient.Web.Features.Blocks.Components.YouTube;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.SectionHero
{
    /// <summary>
    /// Used to insert a section hero block component
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Section Hero",
        GUID = "e206c472-0268-4436-b255-506647a673ed",
        Description = "Section hero component")]
    [ImageUrl("~/icons/score/epi_score128_section_hero.png")]
    [DisplayOptions(new[] {
        DisplayOptionConstants.DisplayOptionNames.EdgeToEdge,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Offset,
        DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half
    })]
    [IndexInContentAreas]
    public class SectionHeroBlock : BaseBlock, INestedContentBlock, IPageContentBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 10
        )]
        //[UIHint(UIHint.Image)]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia), typeof(VimeoBlock), typeof(YouTubeBlock) })]
        [DefaultDragAndDropTarget]
        public virtual ContentReference Media { get; set; }

        [Display(
            Order = 20,
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [Searchable]
        public virtual string Title { get; set; }

        [Display(
            Order = 30,
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Searchable]
        public virtual string Subtitle { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 40)]
        [CultureSpecific]
        [Searchable]
        public virtual XhtmlString Body { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Call To Action Content Area",
            Order = 50)]
        [CultureSpecific]
        [AllowedTypes(typeof(ICallToActionBlock))]
        public virtual ContentArea CallToActionContentArea { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Narrow Media?",
            Order = 70)]
        [CultureSpecific]
        [FullRefresh]
        [OptionBarItem]
        public virtual bool NarrowMedia { get; set; }

        [Display(GroupName = TabNames.Styles, Order = 70, Name = "Text Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(FontColorPickerEditorDescriptor))]
        [UIHint("FontColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string TextColor { get; set; }

        [Display(GroupName = TabNames.Styles, Order = 80, Name = "Background Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(ColorPickerEditorDescriptor))]
        [UIHint("ColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string BackgroundColor { get; set; }

        [Display(GroupName = TabNames.Styles,
            Order = 85,
            Name = "Section Hero Style")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("SectionHeroClasses")]
        public virtual string SectionHeroStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(SectionHeroStyle))
            {
                classes += $" {SectionHeroStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            Title = "[Section Hero]";
            SectionHeroStyle = "default";
            NarrowMedia = false;
        }
    }
}
