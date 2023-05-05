namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    public class DeviceSpec
    {
        public int width { get; set; } // crop width
        public int height { get; set; } // crop height
        public string device { get; set; } // device key
        public string srcSet { get; set; } // srcSet query
    }
}
