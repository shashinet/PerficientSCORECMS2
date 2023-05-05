using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using EPiServer.Validation;
using Perficient.Infrastructure.Templates;
using Perficient.Infrastructure.Templates.Interfaces;

namespace Perficient.Infrastructure.Templates.Validators
{
    /// <summary>
    /// Raise a warning whenever editors decide to change the existing template
    /// </summary>
    public class ContentHasTemplateValidator : IValidate<ITemplateContent>
    {
        public IEnumerable<ValidationError> Validate(ITemplateContent instance)
        {
            if (instance.SelectedTemplate?.ID != instance.OldTemplate?.ID)
            {
                return new[]
                {
                    new ValidationError()
                    {
                        ErrorMessage = "Warning! Changing template can modify all of your previous data",
                        PropertyName = instance.GetPropertyName(p => p.SelectedTemplate),
                        Severity = ValidationErrorSeverity.Warning
                    }
                };
            }
            return Enumerable.Empty<ValidationError>();
        }
    }
}
