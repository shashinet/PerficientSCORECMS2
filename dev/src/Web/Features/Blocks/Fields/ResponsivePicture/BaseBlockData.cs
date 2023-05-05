using Newtonsoft.Json;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    public class BaseBlockData
    {
        [JsonProperty("blockId")]
        public string BlockId { get; set; }

        [JsonProperty("blockParentId")]
        public int BlockParentId { get; set; }

        [JsonProperty("blockName")]
        public string BlockName { get; set; }
    }
}
