using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Picture;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.ResponsivePicture;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.DocumentHeader
{
    /// <summary>
    /// Used to insert a document header component
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Document Header",
        GUID = "65d2536f-180d-4be0-adc2-c3dfed62debc",
        Description = "Document header component.")]
    [ImageUrl("~/icons/score/epi_score128_titleWithSubtitle.png")]
    [DisplayOptions(new[] {
       DisplayOptionConstants.DisplayOptionNames.EdgeToEdge,
            DisplayOptionConstants.DisplayOptionNames.Contained,
            DisplayOptionConstants.DisplayOptionNames.Offset,
            DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half
    })]
    [IndexInContentAreas]
    public class DocumentHeaderBlock : BaseBlock, INestedContentBlock, IPageContentBlock, IOnPageEditHelperPanel
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Responsive Image",
            Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(ResponsivePictureEditorDescriptor))]
        [HideOnContentCreate]
        [CropPoint(1920, 350, "Large", "(min-width: 1441px)")]
        [CropPoint(1490, 350, "Desktop", "(min-width: 1025px) and (max-width: 1440px)")]
        [CropPoint(1075, 350, "Tablet", "(min-width: 601px) and (max-width: 1024px)")]
        [CropPoint(650, 350, "Mobile", "(max-width: 600px)")]
        public virtual ScorePictureFieldBaseBlock ResponsiveImage { get; set; }

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
        public virtual string Subtitle { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Order = 30,
            Name = "Document Header Style"
        )]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("DocumentHeaderClasses")] // name of the class selection property on ScoreSettingsPage
        public virtual string DocumentHeaderStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(DocumentHeaderStyle))
            {
                classes += $" {DocumentHeaderStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            Title = "[Document Header]";
            DocumentHeaderStyle = "default";
        }
    }
}
