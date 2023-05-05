using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAbstraction.Internal;
using EPiServer.DataAbstraction.RuntimeModel;
using EPiServer.Framework.Cache;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Perficient.Infrastructure.Services
{
    public class RestrictedContentTypeAvailabilityService : DefaultContentTypeAvailablilityService
    {
        private readonly IContentLoader _contentLoader;
        private readonly ISiteDefinitionResolver _siteDefinitionResolver;

        public RestrictedContentTypeAvailabilityService(
            ServiceAccessor<IContentTypeRepository> contentTypeRepositoryAccessor,
            IAvailableModelSettingsRepository modelRepository,
            IAvailableSettingsRepository typeSettingsRepository,
            GroupDefinitionRepository groupDefinitionRepository,
            IContentLoader contentLoader,
            ISynchronizedObjectInstanceCache cache,
            ISiteDefinitionResolver siteDefinitionResolver)
            : base(contentTypeRepositoryAccessor,
                   modelRepository,
                   typeSettingsRepository,
                   groupDefinitionRepository,
                   contentLoader,
                   cache)
        {
            _contentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
            _siteDefinitionResolver = siteDefinitionResolver ?? throw new ArgumentNullException(nameof(siteDefinitionResolver));
        }

        // this method is called everytime "Add a new block" or "Add a new page" is called- it fetches the types available
        public override IList<ContentType> ListAvailable(IContent content, bool contentFolder, IPrincipal user)
        {
            var baseList = base.ListAvailable(content, contentFolder, user);

            return Filter(baseList, content).ToList();
        }

        // to filter, simply look at each model type being returned and inspect if it has the RestrictTo attribute
        // if it does have the attribute, ensure that the SiteDefinition.Current is contained within the list of
        // allowed websites.  If it is, allow the model to be returned, otherwise do not
        protected virtual IEnumerable<ContentType> Filter(IList<ContentType> contentTypes, IContent content)
        {
            var siteDefinition = content != null
                ? _siteDefinitionResolver.GetByContent(content.ContentLink, false, false)
                : SiteDefinition.Current;

            foreach (var targetType in contentTypes)
            {
                if (siteDefinition == null)
                {
                    yield return targetType;
                }

                var modelType = targetType.ModelType;

                if (modelType == null)
                {
                    yield return targetType;
                }

                // attempt to fetch an instance of RestrictTo from the model
                var attributeVal = (RestrictToAttribute)Attribute.GetCustomAttribute(modelType, typeof(RestrictToAttribute));
                if (attributeVal == null)
                {
                    yield return targetType;
                }

                var currentSite = siteDefinition.Name;

                // compare current site context name against the list of sites in the attribute
                if (attributeVal.Sites.Any(x =>
                    x.Equals(currentSite, StringComparison.InvariantCultureIgnoreCase)))
                {
                    yield return targetType;
                }
            }
        }
    }
}
