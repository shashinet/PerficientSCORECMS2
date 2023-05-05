using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Perficient.Web.Features.Blocks.Fields.GoogleMap
{
    [ContentType(
        DisplayName = "Google Map",
        GUID = "CFCB4202-B054-42C9-8AB9-F5EA186545CE",
        AvailableInEditMode = false)]

    public class GoogleMapBaseBlock : BaseBlock, INestedContentBlock
    {
        private readonly Injected<IConfiguration> Configuration;

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        public virtual double Longitude { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        public virtual double Latitude { get; set; }

        // get api key from web.config
        //TODO: set Key
        [JsonIgnore]
        public string GoogleApiKey => Configuration.Service["ScoreSettings:GoogleMaps:ApiKey"];
    }
}




