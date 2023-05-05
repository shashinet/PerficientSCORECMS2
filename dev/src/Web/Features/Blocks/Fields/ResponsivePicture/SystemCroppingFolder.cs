using EPiServer.Core;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    /// <summary>
    /// Used to logically group pages in the page tree
    /// </summary>
    [ContentType(
        DisplayName = "Cropping Folder",
        GUID = "9f614aef-0108-4616-82d7-ae165455261c",
        Description = "Used to store Picture croppings within the Content Asset Tree.",
        GroupName = GroupNames.Specialized)]
    //[SiteImageUrl(ScoreConst.StaticGraphicsFolderPath + "epi_score128_page_tree-folder.png")]
    //[ContentIcon(ContentIcon.ObjectFolder, ContentIconColor.Danger)]
    public class SystemCroppingFolder : ContentAssetFolder
    {
    }
}
