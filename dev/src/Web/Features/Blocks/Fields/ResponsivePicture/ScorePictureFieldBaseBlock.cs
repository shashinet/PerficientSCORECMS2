using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Web.Features.Media;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    [ContentType(
        DisplayName = "Picture Field",
        GUID = "4d929e84-4c98-4e90-a0a4-38fb9a08b237",
        AvailableInEditMode = false)]
    public class ScorePictureFieldBaseBlock : BlockData, INestedContentBlock
    {
        [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<PictureCropping>))]
        public virtual IList<PictureCropping> Croppings
        {
            get
            {
                return this.GetPropertyValue(x => x.Croppings);
            }
            set
            {
                this.SetPropertyValue(b => b.Croppings, value);
            }
        }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Original Image",
            Order = 50)]
        [AllowedTypes(new[] { typeof(ImageMediaData) })]
        [UIHint(UIHint.Image)]
        public virtual ContentReference OriginalImage { get; set; }

        public bool HasValue()
        {
            return OriginalImage != null;
        }

        public bool IsValid()
        {
            var valid = OriginalImage != null && (Croppings == null || !Croppings.Any());

            return !valid;
        }

        public ContentReference GetCroppingForDevice(string deviceName)
        {
            return Croppings?.FirstOrDefault(x => x.Device == deviceName)?.Image;
        }

        public string GetInvalidReason()
        {
            string retVal = string.Empty;

            if (!IsValid())
            {
                retVal =
                    "No croppings are found. Please use On Page Edit mode and click the Responsive Picture field. Apply croppings by making your selection and finalize with 'Crop & Finish' or remove the current selection with 'Clear Image' to correct this issue.";
            }

            return retVal;
        }
    }

    /// <summary>
    /// Required to map the IList[PictureCropping] Croppings down in PictureField
    /// </summary>
    [PropertyDefinitionTypePlugIn]
    public class PictureListProperty : PropertyList<PictureCropping> { }

    public class PictureCropping
    {
        // Predefined by CropPointAttribute
        public int Width { get; set; }
        public int Height { get; set; }
        public string Device { get; set; }
        public string SrcSet { get; set; }

        // Set by Dojo picture/main.js
        public ContentReference Image { get; set; } // the actual cropped image

        public ContentReference OriginalImage { get; set; } // the original image

    }
}
