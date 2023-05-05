using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Interfaces.Services;
using Perficient.Infrastructure.Services;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Components.PricingCard
{
    public class PricingCardBlockComponent : AsyncBlockComponent<PricingCardBlock>
    {
        private readonly IFeatureExperimentationService _featureExperimentationService;

        public PricingCardBlockComponent(IFeatureExperimentationService featureExperimentationService)
        {
            _featureExperimentationService = featureExperimentationService;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(PricingCardBlock currentContent)
        {
            if (string.IsNullOrWhiteSpace(currentContent.ExperimentationKey))
            {
                return await Task.FromResult(View("~/Features/Blocks/Components/PricingCard/PricingCardBlock.cshtml", currentContent));
            }


            var decision = _featureExperimentationService.GetExperiment(currentContent.ExperimentationKey);
            if (decision == null || decision.Enabled == false)
            {
                return await Task.FromResult(View("~/Features/Blocks/Components/PricingCard/PricingCardBlock.cshtml", currentContent));
            }

            var clone = currentContent.CreateWritableClone() as PricingCardBlock;
            if (clone == null)
            {
                return null;

            }
            var experimentVariables = decision.Variables.ToDictionary();
            clone.MainTitle = experimentVariables["title"].ToString();
            clone.SecondTitle = experimentVariables["subtitle"].ToString();
            clone.Price = experimentVariables["Cost"].ToString();
            return await Task.FromResult(View("~/Features/Blocks/Components/PricingCard/PricingCardBlock.cshtml", clone));
        }
    }
}
