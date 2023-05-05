using System;

namespace Perficient.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ClassSelectionsAttribute : Attribute
    {
        public string SettingsPropertyName { get; set; }

        public ClassSelectionsAttribute(string propertyName)
        {
            this.SettingsPropertyName = propertyName;
        }
    }
}
