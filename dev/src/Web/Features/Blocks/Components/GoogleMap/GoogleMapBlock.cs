using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Fields;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.GoogleMap;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.GoogleMap
{
    /// <summary>
    /// Used to insert a Google map block component
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Google Map",
        GUID = "4410beef-1012-47fd-8509-64c69f972836",
        Description = "Google map component")]
    [ImageUrl("~/icons/score/epi_score128_map.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.OneThird })]
    public class GoogleMapBlock : BaseBlock, INestedContentBlock
    {
        [Display(
            Order = 20,
            GroupName = SystemTabNames.Content,
            Name = "Location Title"
        )]
        [CultureSpecific]
        public virtual string LocationTitle { get; set; }

        [Display(
           GroupName = SystemTabNames.Content,
           Name = "Location Coordinates",
           Order = 40)]
        [EditorDescriptor(EditorDescriptorType = typeof(GoogleMapEditorDescriptor))]
        public virtual GoogleMapBaseBlock LocationCoordinates { get; set; }

        [Display(
            Order = 50,
            GroupName = SystemTabNames.Content,
            Name = "Location Description"
        )]
        [CultureSpecific]
        public virtual string LocationDescription { get; set; }
    }
}
