using EPiServer.Cms.Shell.UI.CompositeViews.Internal;
using EPiServer.Core;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using Perficient.Infrastructure.Templates.Interfaces;
using Perficient.Infrastructure.Templates.Models;
using Perficient.Infrastructure.Templates.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.Templates.Descriptors
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class TemplatesRepositoryDescriptor : ContentRepositoryDescriptorBase
    {
        public static string RepositoryKey => "templates";

        public override IEnumerable<Type> ContainedTypes => (new[]
        {
            typeof(ITemplateBlock),
            typeof(ITemplatePage)
        });

        public override IEnumerable<Type> CreatableTypes => Enumerable.Empty<Type>();

        public override string CustomSelectTitle => LocalizationService.Current.GetString("/contentrepositories/templates/customselecttitle", "Templates");

        public override string Key => RepositoryKey;

        public override IEnumerable<Type> MainNavigationTypes => new[]
        {
           typeof(TemplatesFolder),
           typeof(TemplatesRootFolder),
           typeof(BlockTemplatesFolder),
           typeof(PageTemplatesFolder)
        };

        public override IEnumerable<string> MainViews => new string[1] { HomeView.ViewName };

        public override string Name => LocalizationService.Current.GetString("/contentrepositories/templates/name", "Templates");

        public override IEnumerable<ContentReference> Roots => new[] { Templates.Service.TemplatesRoot };

        public override int SortOrder => 1100;

        private Injected<ITemplatesService> Templates { get; set; }
    }
}
