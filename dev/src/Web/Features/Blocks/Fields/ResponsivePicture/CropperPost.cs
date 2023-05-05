namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    public class CropperPost
    {
        public BaseBlockData baseBlockData { get; set; }

        public int mainImage { get; set; }

        public CropperData data { get; set; }
        public EpiData epiData { get; set; }
        public DeviceSpec deviceSpec { get; set; }
    }
}
