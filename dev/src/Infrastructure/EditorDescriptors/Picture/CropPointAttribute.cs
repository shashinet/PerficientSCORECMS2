using Newtonsoft.Json;
using System;

namespace Perficient.Infrastructure.EditorDescriptors.Picture
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CropPointAttribute : Attribute
    {
        public CropPointAttribute(int width, int height, string device, string srcSet)
        {
            Width = width;
            Height = height;
            Device = device;
            SrcSet = srcSet;
        }

        public int Width { get; }

        public int Height { get; }

        public string Device { get; }

        public string SrcSet { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
