using EPiServer.Core;

namespace Perficient.Infrastructure.Models.Properties
{
    public class ScoreBlobModel
    {
        public string Device { get; set; }
        public string ImageUrl { get; set; }
        public ContentReference Image { get; set; }
    }
}
