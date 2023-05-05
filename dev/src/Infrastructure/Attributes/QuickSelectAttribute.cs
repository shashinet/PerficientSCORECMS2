using EPiServer.Shell.ObjectEditing;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Perficient.Infrastructure.SelectionFactories;
using System;

namespace Perficient.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class QuickSelectAttribute : SelectOneAttribute, IDisplayMetadataProvider
    {
        public string[] QuickListItems { get; set; }

        public QuickSelectAttribute(params string[] listItemsText)
        {
            QuickListItems = listItemsText;
        }

        public new void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            SelectionFactoryType = typeof(QuickListSelectionFactory);
            base.CreateDisplayMetadata(context);
        }
    }
}
