using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using System;

namespace Perficient.Infrastructure.Templates.Models
{
    [ContentType(
        DisplayName = "Templates Root Folder",
        GUID = "7C42D799-0AA3-4082-ACFA-72A695AC851D",
        AvailableInEditMode = false)]
    [AvailableContentTypes(Include = new[]
    {
        typeof(TemplatesRootFolder),
        typeof(PageTemplatesFolder),
        typeof(BlockTemplatesFolder)
    })]
    public class TemplatesRootFolder : ContentFolder
    {
        public const string TemplatesRootName = "TemplatesRoot";
        public static Guid TemplatesRootGuid = new Guid("86ECC904-84AD-420A-9B43-F9CEF693DC1B");

        public const string TemplatesGlobalName = "For All Sites";

        private Injected<LocalizationService> _localizationService;
        private static Injected<ContentRootService> _rootService;

        public static ContentReference TemplatesRoot => GetTemplatesRoots();

        public override string Name
        {
            get
            {
                if (ContentLink.CompareToIgnoreWorkID(TemplatesRoot))
                {
                    return _localizationService.Service.GetString("/contentrepositories/templates/Name", "Templates");
                }
                return base.Name;
            }
            set => base.Name = value;
        }

        private static ContentReference GetTemplatesRoots() => _rootService.Service.Get(TemplatesRootName);
    }
}
