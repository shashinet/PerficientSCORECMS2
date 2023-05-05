using Newtonsoft.Json;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    public class CheckImage
    {
        [JsonProperty("imageName")]
        public string ImageName { get; set; }

        [JsonProperty("baseBlockData")]
        public BaseBlockData BlockData { get; set; }

        [JsonProperty("devices")]
        public List<string> Devices { get; set; }

        [JsonProperty("parentId")]
        public int ParentId { get; set; }

        [JsonProperty("imageId")]
        public int ImageId { get; set; }
    }
}
