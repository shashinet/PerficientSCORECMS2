using EPiServer.Core;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.ExperimentTracking
{
    [ContentType(
    GroupName = GroupNames.Experimentation,
    DisplayName = "Experiment Tracking",
    GUID = "BFDB4558-FD47-4F87-A143-5F9168F56D8E",
    Description = "Tracks sucessful feature experimentations"
    )]
    [ImageUrl("~/icons/score/epi_score128_introduction.png")]
    public class ExperimentTrackingBlock : BlockData, IPageContentBlock, INestedContentBlock
    {
        [Display(
         Order = 70,
         GroupName = TabNames.Experiment,
         Name = "Feature Experimentation Key"
         )]
        public virtual string ExperimentationKey { get; set; }
    }
}

