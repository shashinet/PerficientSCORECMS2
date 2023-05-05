using System;

namespace Perficient.Infrastructure.Attributes
{
    //TODO: Add Restrict To Site Service
    /*
        Usage:
        Use this attribute to decorate the page type or block type (above the class definition)
        to restrict usage of that object to defined site.

        [RestrictTo(new[] { 'Site name' })]

    */

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RestrictToAttribute : Attribute
    {
        public string[] Sites { get; set; }

        public RestrictToAttribute(string[] sites)
        {
            Sites = sites;
        }
    }
}
