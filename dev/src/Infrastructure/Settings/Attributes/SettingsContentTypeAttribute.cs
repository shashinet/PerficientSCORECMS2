using EPiServer.DataAnnotations;
using System;

namespace Perficient.Infrastructure.Settings.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public sealed class SettingsContentTypeAttribute : ContentTypeAttribute
    {
        public string SettingsName { get; set; }
    }
}
