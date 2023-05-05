using System;

namespace Perficient.Infrastructure.DisplayOptions.Attributes
{
    /// <summary>
    /// Indicate a class is used to provide DisplayOptionDefinitions with constant fields
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DisplayOptionsProviderAttribute : Attribute
    {
    }
}
