using OptimizelySDK;
using OptimizelySDK.Entity;
using OptimizelySDK.OptimizelyDecisions;

namespace Perficient.Infrastructure.Interfaces.Services
{
    public interface IFeatureExperimentationService
    {
        public OptimizelyDecision GetExperiment(string testName,
            OptimizelyUserContext user = null,
            UserAttributes userAttributes = null,
            EventTags eventTags = null);
                
        public OptimizelyUserContext CreateUserContext(UserAttributes userAttributes = null, EventTags eventTags = null);
        public string GetUserId();

        public void TrackEvent(string eventKey);

    }
}
