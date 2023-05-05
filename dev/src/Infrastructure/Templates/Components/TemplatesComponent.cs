using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;
using Perficient.Infrastructure.Templates.Descriptors;

namespace Perficient.Infrastructure.Templates.Components
{
    [Component]
    public sealed class TemplatesComponent : ComponentDefinitionBase
    {
        public TemplatesComponent() :
            base("templates/component/widget/templates")
        {
            LanguagePath = "/episerver/cms/components/templates";
            Title = "Templates";
            SortOrder = 1100;
            PlugInAreas = new [] { PlugInArea.AssetsDefaultGroup };
            Settings
                .Add(new Setting("repositoryKey",
                    value: TemplatesRepositoryDescriptor.RepositoryKey));
        }
    }
}
