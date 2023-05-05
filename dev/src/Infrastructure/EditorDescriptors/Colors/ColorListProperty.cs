using EPiServer.Core;
using EPiServer.PlugIn;
using Perficient.Infrastructure.Models.Properties;

namespace Perficient.Infrastructure.EditorDescriptors.Colors
{
    [PropertyDefinitionTypePlugIn]
    public class ColorListProperty : PropertyList<ScoreColor>
    {
    }
}
