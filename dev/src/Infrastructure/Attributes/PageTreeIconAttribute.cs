using System;

namespace Perficient.Infrastructure.Attributes
{
    public class PageTreeIconAttribute : Attribute
    {
        public string IconClass { get; set; }
        public PageTreeIconAttribute(string iconClass)
        {
            IconClass = iconClass;
        }
    }
}
