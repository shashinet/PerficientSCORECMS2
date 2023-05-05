using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Interfaces.Services;
using System.Threading.Tasks;


namespace Perficient.Web.Features.Blocks.Components.ExperimentTracking
{
    public class ExperimentTrackingBlockComponent : AsyncBlockComponent<ExperimentTrackingBlock>
    {
        private readonly IFeatureExperimentationService _featureExperimentationService;
        private readonly bool _isInEditMode;

        public ExperimentTrackingBlockComponent(IFeatureExperimentationService featureExperimentationService, IsInEditModeAccessor isInEditModeAccessor)
        {
            _featureExperimentationService = featureExperimentationService;
            _isInEditMode = isInEditModeAccessor();
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(ExperimentTrackingBlock currentContent)
        {
            _featureExperimentationService.TrackEvent(currentContent.ExperimentationKey);
            return await Task.FromResult(View("~/Features/Blocks/Components/ExperimentTracking/ExperimentTrackingBlock.cshtml", currentContent));
        }
    }
}
