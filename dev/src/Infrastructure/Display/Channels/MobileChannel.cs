using EPiServer.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Perficient.Infrastructure.Display.Resolutions;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;


namespace Perficient.Infrastructure.Display.Channels
{
    public class MobileChannel : DisplayChannel
    {
        public override string ChannelName => "Mobile";

        public override string ResolutionId => typeof(IphoneVerticalResolution).FullName;

        public override bool IsActive(HttpContext context)
        {
            var detection = context.RequestServices.GetRequiredService<IDetectionService>();
            return detection.Device.Type == Device.Mobile;
        }
    }
}
