using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Picture;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.ResponsivePicture;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.ResponsiveImage
{
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Responsive Image",
        GUID = "ecd5ba1f-946a-496e-9ba5-356893133003",
        Description = "Responsive image component")]
    [ImageUrl("~/icons/score/epi_score128_responsive_image.png")]
    public class ResponsiveImageBlock : BaseBlock, IHeroBlock, INestedContentBlock, IContentPublished, IOnPageEditHelperPanel
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Responsive Image",
            Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(ResponsivePictureEditorDescriptor))]
        [HideOnContentCreate]
        [CropPoint(1920, 640, "Large", "(min-width: 1560px)")]
        [CropPoint(1600, 640, "Desktop", "(min-width: 1260px) and (max-width: 1559px)")]
        [CropPoint(1259, 640, "Tablet", "(min-width: 801px) and (max-width: 1259px)")]
        [CropPoint(858, 384, "Mobile", "(max-width: 800px)")]
        public virtual ScorePictureFieldBaseBlock ResponsiveImage { get; set; }


        public void PublishedContent(object sender, ContentEventArgs e)
        {
            if (ResponsiveImage != null && !ResponsiveImage.IsValid())
            {
                e.CancelAction = true;
                e.CancelReason =
                    $"Responsive Image is not configured correctly. {ResponsiveImage.GetInvalidReason()}";
            }
        }
    }
}
