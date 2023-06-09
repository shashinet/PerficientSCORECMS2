using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace Perficient.Infrastructure.Display.Resolutions
{
    public abstract class DisplayResolutionBase : IDisplayResolution
    {
        private readonly Injected<LocalizationService> LocalizationService;

        protected DisplayResolutionBase(string name, int width, int height)
        {
            Id = GetType().FullName;
            Name = Translate(name);
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets the unique ID for this resolution
        /// </summary>
        public string Id { get; protected set; }

        /// <summary>
        /// Gets the name of resolution
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the resolution width in pixels
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets the resolution height in pixels
        /// </summary>
        public int Height { get; protected set; }

        private string Translate(string resurceKey)
        {

            if (!LocalizationService.Service.TryGetString(resurceKey, out var value))
            {
                value = resurceKey;
            }

            return value;
        }
    }
}
