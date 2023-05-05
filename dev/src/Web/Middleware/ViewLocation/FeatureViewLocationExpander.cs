using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Perficient.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Web.Middleware.ViewLocation
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        #region Const View Path parts
        private const string ChildFeature = "childFeature";
        private const string Feature = "feature";
        private const string SubfolderKey = "{subfolder}";
        private const string FeaturesRoot = "/Features";
        private const string BlocksRoot = $"{FeaturesRoot}/Blocks";
        private const string NavigationRoot = $"{FeaturesRoot}/Navigation";
        private const string PagesRoot = $"{FeaturesRoot}/Pages";
        private const string SharedRoot = $"{FeaturesRoot}/Shared";
        #endregion
        #region View Path formats
        #region Paths and Subfolder mappings
        private readonly List<string> _blockSubFolders = new List<string>
        {
            "Collections",
            "Components",
            "Fields",
            "Layouts"
        };
        private readonly List<string> _blockViewLocationFormats = new List<string>
        {
            $"{BlocksRoot}/{SubfolderKey}/{{1}}Block.cshtml",
            $"{BlocksRoot}/{SubfolderKey}/{{1}}/{{0}}.cshtml",
            $"{BlocksRoot}/{SubfolderKey}/{{1}}/{{1}}Block.cshtml",
        };
        private IEnumerable<string> _blockViewLocations
        {
            get
            {
                return GetPathsWithSubfolders(_blockViewLocationFormats, _blockSubFolders);
            }
        }

        private readonly List<string> _pageSubFolders = new List<string>();
        private readonly List<string> _pageViewLocationFormats = new List<string>
        {
            $"{PagesRoot}/{{1}}.cshtml",
            $"{PagesRoot}/{{1}}Page.cshtml",
            $"{PagesRoot}/{{1}}/{{1}}Page.cshtml",
            $"{PagesRoot}/{{1}}/{{1}}.cshtml",
            $"{PagesRoot}/{{1}}/{{0}}.cshtml",
            $"{PagesRoot}/{{1}}/Index.cshtml",
            $"{PagesRoot}/{SubfolderKey}/{{1}}.cshtml",
            $"{PagesRoot}/{SubfolderKey}/{{1}}Page.cshtml",
            $"{PagesRoot}/{SubfolderKey}/{{1}}/{{1}}Page.cshtml",
            $"{PagesRoot}/{SubfolderKey}/{{1}}/{{1}}.cshtml",
            $"{PagesRoot}/{SubfolderKey}/{{1}}/{{0}}.cshtml",
        };
        private IEnumerable<string> _pageViewLocations
        {
            get
            {
                return GetPathsWithSubfolders(_pageViewLocationFormats, _pageSubFolders);
            }
        }

        private readonly List<string> _sharedSubFolders = new List<string>
        {
            "DisplayTemplates",
            "Views"
        };
        private readonly List<string> _sharedViewLocationFormats = new List<string>
        {
            $"{SharedRoot}/{{0}}.cshtml",
            $"{SharedRoot}/{SubfolderKey}/{{0}}.cshtml",
            $"{SharedRoot}/{SubfolderKey}/{{1}}/{{0}}.cshtml"
        };
        private IEnumerable<string> _sharedViewLocations
        {
            get
            {
                return GetPathsWithSubfolders(_sharedViewLocationFormats, _sharedSubFolders);
            }
        }
        #endregion

        private readonly List<string> _featuresViewLocationFormats = new List<string>
        {
            "/Features/{3}/{1}/{0}.cshtml",
            "/Features/{3}/{0}.cshtml",
            "/Features/{3}/{4}/{1}/{0}.cshtml",
            "/Features/{3}/{4}/{0}.cshtml",
        };

        private readonly List<string> _standardViewLocationFormats = new List<string>()
        {
            "/Cms/Views/{1}/{0}.cshtml",
            $"{NavigationRoot}/Views/{{0}}.cshtml",
            $"{NavigationRoot}/Views/{{1}}.cshtml",
            $"{NavigationRoot}/Views/DisplayTemplates/{{0}}.cshtml",
            $"{NavigationRoot}/Views/DisplayTemplates/{{1}}.cshtml",
            "/FormsViews/Views/ElementBlocks/{0}.cshtml"
        };
        #endregion
        #region Core Methods
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            return ExpandViewLocationsIterator(context, viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var controllerActionDescriptor = context.ActionContext?.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null || !controllerActionDescriptor.Properties.ContainsKey(Feature))
            {
                return;
            }
            context.Values[Feature] = controllerActionDescriptor?.Properties[Feature].ToString();

            if (controllerActionDescriptor.Properties.ContainsKey(ChildFeature))
            {
                context.Values[ChildFeature] = controllerActionDescriptor?.Properties[ChildFeature].ToString();
            }
        }
        #endregion

        private IEnumerable<string> ExpandViewLocationsIterator(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            // these come first because a controllerless view called from any view or controller will pass through the feature convention
            // and get caught up in the second block with the calling, unrelated controller. Treat a ViewName special here for pages and blocks.
            if (!string.IsNullOrWhiteSpace(context.ViewName))
            {
                var locations = AddStandardViewLocations(Enumerable.Empty<string>());
                if (context.ViewName.EndsWith("Page"))
                {
                    locations = ExtendPageBlockViewLocations(_pageViewLocations.Concat(locations), context.ViewName.TrimEnd("Page"));
                }
                else if (context.ViewName.EndsWith("Block"))
                {
                    locations = ExtendPageBlockViewLocations(_blockViewLocations.Concat(locations), context.ViewName.TrimEnd("Block"));
                }
                foreach (var location in locations)
                {
                    yield return location;
                }
            }

            if (context.ActionContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor
                && controllerActionDescriptor.Properties.ContainsKey(Feature))
            {
                string featureName = controllerActionDescriptor.Properties[Feature] as string;
                string childFeatureName = null;

                if (controllerActionDescriptor.Properties.ContainsKey(ChildFeature))
                {
                    childFeatureName = controllerActionDescriptor.Properties[ChildFeature] as string;
                }
                var viewLocationFormats = _standardViewLocationFormats.Concat(AddStandardViewLocations(_featuresViewLocationFormats)).Concat(viewLocations);
                foreach (var item in ExtendFeatureViewLocations(viewLocationFormats, featureName, childFeatureName))
                {
                    yield return item;
                }
            }
            else
            {
                foreach (var location in viewLocations)
                {
                    yield return location;
                }
            }
        }

        private IEnumerable<string> GetPathsWithSubfolders(IEnumerable<string> viewLocationPaths, IEnumerable<string> subFolders)
        {
            var viewLocations = new List<string>();
            var subFolderPaths = viewLocationPaths.ToLookup(p => p.Contains(SubfolderKey));
            viewLocations.AddRange(
                subFolders.SelectMany(folder => subFolderPaths[true].Select(p => p.Replace(SubfolderKey, folder)))
            );
            viewLocations.AddRange(subFolderPaths[false]);
            return viewLocations;
        }

        private IEnumerable<string> AddStandardViewLocations(IEnumerable<string> viewLocations)
        {
            return viewLocations.Concat(_standardViewLocationFormats).Concat(_sharedViewLocations);
        }

        private IEnumerable<string> ExtendPageBlockViewLocations(IEnumerable<string> viewLocations, string folderName)
        {
            var folderLocations = viewLocations.Where(vl => vl.Contains("{1}"));
            var updatedLocations = folderLocations.Select(l => l.Replace("{1}", folderName));
            foreach (var location in updatedLocations.Concat(viewLocations))
            {
                var updatedLocation = location.Replace("{1}", folderName);
                yield return updatedLocation;
            }
        }
        private IEnumerable<string> ExtendFeatureViewLocations(IEnumerable<string> viewLocations, string featureName, string childFeatureName)
        {
            foreach (var location in viewLocations)
            {
                var updatedLocation = location.Replace("{3}", featureName);
                if (location.Contains("{4}") && string.IsNullOrEmpty(childFeatureName))
                {
                    continue;
                }
                else
                {
                    updatedLocation = updatedLocation.Replace("{4}", childFeatureName);
                }
                yield return updatedLocation;
            }
        }
    }
}
