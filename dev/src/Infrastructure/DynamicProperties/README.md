using System;
using System.Collections.Generic;

using Perficient.Infrastructure.DynamicProperties.Abstracts;
using Perficient.Infrastructure.DynamicProperties.Models;
using Perficient.Infrastructure.Pages.ErrorPage;

namespace Perficient.Infrastructure.Blocks.Collections.Accordion
{
    public class AccordionBlockDynamicProperties : DynamicPropertiesRegistration
    {
        public override Type ForType { get; set; } = typeof(AccordionBlock);

        public override Dictionary<Type, string[]> OtherRegisteredTypes { get; set; } = new Dictionary<Type, string[]>
        {
            { typeof(ErrorPage), new string[] {nameof(ErrorPage.Accordion), nameof(ErrorPage.AnotherAccordionTest) } },
        };

        public override void RegisterDynamicProperties()
        {
            var themeDynamicProperty = new DynamicPropertyRegistratorModel(nameof(AccordionBlock.ThemeSelection));
            themeDynamicProperty.HideFields.Add("Theme One", new string[] { nameof(AccordionBlock.ThemeTwoValue), nameof(AccordionBlock.ThemeThreeValue) });
            themeDynamicProperty.HideFields.Add("Theme Two", new string[] { nameof(AccordionBlock.ThemeOneValue), nameof(AccordionBlock.ThemeThreeValue), nameof(AccordionBlock.CheckboxTest) });
            themeDynamicProperty.HideFields.Add("Theme Three", new string[] { nameof(AccordionBlock.ThemeTwoValue), nameof(AccordionBlock.ThemeOneValue), nameof(AccordionBlock.CheckboxTest) });

            themeDynamicProperty.ShowFields.Add("Theme One", new string[] { nameof(AccordionBlock.ThemeOneValue), nameof(AccordionBlock.CheckboxTest) });
            themeDynamicProperty.ShowFields.Add("Theme Two", new string[] { nameof(AccordionBlock.ThemeTwoValue) });
            themeDynamicProperty.ShowFields.Add("Theme Three", new string[] { nameof(AccordionBlock.ThemeThreeValue) });

            DynamicProperties.Add(themeDynamicProperty);

            var checkboxDynamicProperty = new DynamicPropertyRegistratorModel(nameof(AccordionBlock.CheckboxTest));
            checkboxDynamicProperty.HideFields.Add(false.ToString(), new string[] { nameof(AccordionBlock.CheckboxShowOnTrue) });
            checkboxDynamicProperty.ShowFields.Add(true.ToString(), new string[] { nameof(AccordionBlock.CheckboxShowOnTrue) });

            DynamicProperties.Add(checkboxDynamicProperty);
        }
    }
}
