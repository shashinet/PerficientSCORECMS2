using EPiServer.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Perficient.Infrastructure.Display.Channels
{
    public class WebChannel : DisplayChannel
    {
        public override string ChannelName => "Web";

        public override bool IsActive(HttpContext context)
        {
            var detection = context.RequestServices.GetRequiredService<IDetectionService>();
            return detection.Device.Type == Device.Desktop;
        }
    }
}
