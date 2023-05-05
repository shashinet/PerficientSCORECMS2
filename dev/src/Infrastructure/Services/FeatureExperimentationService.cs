using Microsoft.AspNetCore.Http;
using OptimizelySDK;
using OptimizelySDK.Entity;
using OptimizelySDK.OptimizelyDecisions;
using Perficient.Infrastructure.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Perficient.Infrastructure.Services
{
    public class FeatureExperimentationService : IFeatureExperimentationService
    {
        private readonly IOptimizely _featureExpermentation;
        private readonly ICookieService _cookieService;
        private const string cookieName = "feature-experimentation-user";
        private readonly bool _isInEditMode;
        private readonly IEnumerable<string> _allowedRoles = new string[] { "Administrator", "CmsAdmins", "CmsEditors" };

        private bool _editorLoggedIn                     
        {
            get {
                //This can be simplified
                foreach (string role in _allowedRoles)
                {
                    if (EPiServer.Security.PrincipalInfo.CurrentPrincipal.IsInRole(role))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public FeatureExperimentationService(IOptimizely featureExpermentation,
            ICookieService cookieService,
            IsInEditModeAccessor isInEditModeAccessor)
        {
            _featureExpermentation = featureExpermentation;
            _cookieService = cookieService;
            _isInEditMode = isInEditModeAccessor();            
        }

        
                
        public OptimizelyDecision GetExperiment(string testName, 
            OptimizelyUserContext user = null,            
            UserAttributes userAttributes = null, 
            EventTags eventTags = null)
        {
            if (string.IsNullOrWhiteSpace(testName) || _isInEditMode || _editorLoggedIn)
            {
                return null;
            }

            if (user == null)
            {
                user = this.CreateUserContext(userAttributes, eventTags);
            }
                        
            OptimizelyDecision decision = user.Decide(testName);
            return decision;
        }
        
        public OptimizelyUserContext CreateUserContext(UserAttributes userAttributes = null, EventTags eventTags = null)
        {
            return _featureExpermentation.CreateUserContext(GetUserId());
        }

        public string GetUserId()
        {
            var userId = _cookieService.Get(cookieName);
            if (userId == null)
            { 
                userId = Guid.NewGuid().ToString();
                _cookieService.Set(cookieName, userId);
            }
            
            return userId;
        }

        public void TrackEvent(string eventKey)
        {
            if (!_isInEditMode && !_editorLoggedIn)
            {
                var user = this.CreateUserContext();                
                user.TrackEvent(eventKey);
            }
        }
    }
}
