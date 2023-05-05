using System;

namespace Perficient.Infrastructure.Attributes
{
    //TODO - Add Tiny MCE Congfig Service
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TinyMceConfigAttribute : Attribute
    {
        public string ConfigName { get; internal set; }

        public TinyMceConfigAttribute(string configName)
        {
            ConfigName = configName;
        }
    }
}
