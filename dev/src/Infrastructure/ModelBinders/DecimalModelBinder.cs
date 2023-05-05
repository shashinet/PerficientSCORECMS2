using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Perficient.Infrastructure.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string modelName = bindingContext.ModelName;
            string attemptedValue =
                bindingContext.ValueProvider.GetValue(modelName).FirstValue;

            // Depending on CultureInfo, the NumberDecimalSeparator can be "," or "."
            // Both "." and "," should be accepted, but aren't.
            string wantedSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            string alternateSeparator = (wantedSeparator == "," ? "." : ",");

            if (!attemptedValue.Contains(wantedSeparator, StringComparison.CurrentCulture)
                && !attemptedValue.Contains(alternateSeparator, StringComparison.CurrentCulture))
            {
                attemptedValue = attemptedValue.Replace(alternateSeparator, wantedSeparator);
            }

            if (bindingContext.ModelMetadata.IsNullableValueType
                && string.IsNullOrWhiteSpace(attemptedValue))
            {
                return;
            }

            try
            {
                bindingContext.Result = ModelBindingResult.Success(decimal.Parse(attemptedValue, NumberStyles.Any));
            }
            catch (FormatException e)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                bindingContext.ModelState.AddModelError(modelName, e.Message);
            }

            await Task.CompletedTask;
        }
    }
}
