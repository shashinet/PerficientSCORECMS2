using EPiServer.Shell.Services.Rest;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.DynamicProperties.Interface;

namespace Perficient.Infrastructure.DynamicProperties.Controllers
{
    [ApiController]
    [Route("episerver/cms/customstores/dynamicproperties")]
    public class DynamicPropertiesController : ControllerBase
    {
        private readonly IDynamicPropertiesService _dynamicPropertiesService;

        public DynamicPropertiesController(IDynamicPropertiesService dynamicPropertiesService)
        {
            _dynamicPropertiesService = dynamicPropertiesService;
        }

        [HttpGet]
        [Route("getdynamicproperties/{typeIdentifier}")]
        public RestResult GetDynamicProperties(string typeIdentifier)
        {
            return new RestResult
            {
                Data = _dynamicPropertiesService.RetrieveDynamicPropertiesForType(typeIdentifier)
            };
        }
    }
}
