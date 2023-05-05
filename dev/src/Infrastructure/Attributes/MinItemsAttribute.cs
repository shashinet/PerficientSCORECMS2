using EPiServer.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Perficient.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinItemsAttribute : ValidationAttribute
    {
        private readonly int _minAllowed;

        public MinItemsAttribute(int MinItemsAllowed)
        {
            _minAllowed = MinItemsAllowed;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contentArea = value as ContentArea;

            // Get all items or none if null
            var allItems = contentArea?.Items ?? Enumerable.Empty<ContentAreaItem>();

            // Count the unique personalisation group names, replacing empty ones (items which aren't personalised) with a unique name
            var i = 0;
            var minNumberOfItemsShown = allItems
                .Select(x => string.IsNullOrEmpty(x.ContentGroup) ? (i++).ToString() : x.ContentGroup)
                .Distinct()
                .Count();

            return (minNumberOfItemsShown < _minAllowed) ? new ValidationResult($"The property \"{validationContext.DisplayName}\" must have at least {_minAllowed} items") : null;
        }
    }
}
