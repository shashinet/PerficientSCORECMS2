using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Picture;
using Perficient.Infrastructure.Extensions;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.ResponsivePicture;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Pages.BaseContentPage
{
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.ThreeFourth,
        DisplayOptionConstants.DisplayOptionNames.Full })]
    public abstract class BaseContentPage : BasePage
    {
        [Display(
            GroupName = TabNames.Global,
            Name = "Content Image",
            Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(ResponsivePictureEditorDescriptor))]
        [HideOnContentCreate]
        [CropPoint(1136, 1136, ContentImageNames.HeroDesktop, "(min-width: 1200px)")]
        [CropPoint(650, 650, ContentImageNames.HeroMobile, "(max-width: 1199px)")]        
        [CropPoint(400, 300, ContentImageNames.Card, null)]        
        [CropPoint(1200, 638, ContentImageNames.OpenGraph, null)]
        [OptionBarItem]
        [FullRefresh]
        public virtual ScorePictureFieldBaseBlock ContentImage { get; set; }

        [CultureSpecific]
        [Display(Name = "Title", GroupName = TabNames.Global, Order = 20)]
        public virtual string Title
        {
            get
            {
                var contentTitle = this.GetPropertyValue(p => p.Title);

                if (!string.IsNullOrEmpty(contentTitle)) return contentTitle;

                if (!string.IsNullOrEmpty(MetaTitle)) return MetaTitle;

                return PageName;
            }
            set => this.SetPropertyValue(p => p.Title, value);
        }

        [ScaffoldColumn(false)]
        public virtual string ContentSummary
        {
            get
            {
                //If the content type has a summary field on it, then take that field else take the page description
                string summary;
                var hasProperty = this.TryGetPropertyValue("Summary", out summary);
                if (hasProperty && !string.IsNullOrWhiteSpace(summary))
                {
                    return summary;
                }

                return this.PageDescription;
            }
        }


        [CultureSpecific]
        [Display(Name = "Content Link Text", GroupName = TabNames.Global, Order = 30)]
        public virtual string ContentCtaText
        {
            get
            {
                var ctaText = this.GetPropertyValue(p => p.ContentCtaText);

                //TODO: CTA Default Text should be localizable
                return !string.IsNullOrWhiteSpace(ctaText)
                    ? ctaText
                    : "Learn More";
            }
            set => this.SetPropertyValue(p => p.ContentCtaText, value);
        }
    }
}
