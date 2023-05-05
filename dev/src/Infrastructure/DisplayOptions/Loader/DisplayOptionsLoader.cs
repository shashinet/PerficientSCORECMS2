using EPiServer.Logging;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Perficient.Infrastructure.DisplayOptions.Loader
{
    public static class DisplayOptionsLoader
    {
        private static ILogger _logger = LogManager.GetLogger(typeof(DisplayOptionsLoader));

        // load display options from IDisplayOptionsProvider and decorated Constants and add to Episerver DisplayOptions in specified order
        public static void LoadDisplayOptions(EPiServer.Web.DisplayOptions epiDisplayOptions)
        {
            if (epiDisplayOptions == null)
            {
                _logger.Information("[DisplayOptionsLoader]:[LoadDisplayOptions] - Display Options are null.");
                throw new ArgumentNullException(nameof(epiDisplayOptions));
            }

            var appAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic);

            var displayOptionDefinitions = getDefinitionsFromProviders(appAssemblies).Concat(getDecoratedDefinitions(appAssemblies));

            foreach (var option in displayOptionDefinitions.OrderBy(o => o.Order))
            {
                epiDisplayOptions.Add(option.Name.ToLower(), option.Name, option.RenderingTag, "", option.IconClass);
            }
        }

        private static List<Perficient.Infrastructure.DisplayOptions.Models.DisplayOption> getDefinitionsFromProviders(IEnumerable<Assembly> assemblies)
        {
            if (assemblies?.FirstOrDefault() == null) { return new List<Perficient.Infrastructure.DisplayOptions.Models.DisplayOption>(); }

            // get all classes defined as providers with interface and call get list to retrieve all items
            var optionsProviders = assemblies.SelectMany(a => a.GetLoadableTypes()).Where(t => typeof(IDisplayOptionsProvider).IsAssignableFrom(t) && !t.IsInterface);
            _logger.Debug($"[DisplayOptionsLoader]:[getDefinitionsFromProviders] - Options Provider Count: {optionsProviders.Count()}.");

            var optionsList = optionsProviders.Select(op => (IDisplayOptionsProvider)Activator.CreateInstance(op)).SelectMany(p => p.GetList());
            _logger.Debug($"[DisplayOptionsLoader]:[getDefinitionsFromProviders] - Options List Count: {optionsList.Count()}.");

            return optionsList.ToList();
        }

        private static List<Perficient.Infrastructure.DisplayOptions.Models.DisplayOption> getDecoratedDefinitions(IEnumerable<Assembly> assemblies)
        {
            if (assemblies?.FirstOrDefault() == null) { return new List<Models.DisplayOption>(); }

            var optionsProviders = assemblies.SelectMany(a => a.GetLoadableTypes()).Where(t => t.IsDefined(typeof(DisplayOptionsProviderAttribute)));
            _logger.Debug($"[DisplayOptionsLoader]:[getDecoratedDefinitions] - Options Provider Count: {optionsProviders.Count()}.");

            var optionFields = optionsProviders.SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                .Where(f => f.IsLiteral && f.FieldType == typeof(string) && f.IsDefined(typeof(DisplayOptionDefinitionAttribute))));
            _logger.Debug($"[DisplayOptionsLoader]:[getDecoratedDefinitions] - Options Fields Count: {optionFields.Count()}.");

            if (!optionFields.Any()) { return new List<Models.DisplayOption>(); }

            var optionsList = new List<Models.DisplayOption>();
            // normalize options for sorting and adding later
            foreach (var optionField in optionFields)
            {
                var doAttribute = optionField.GetCustomAttribute<DisplayOptionDefinitionAttribute>();
                _logger.Debug($"[DisplayOptionsLoader]:[getDecoratedDefinitions] - Adding Display Option. Name: {optionField.GetValue(null) as string}. Rendering Tag: {doAttribute.RenderingTag}. Icon Class: {doAttribute.IconClass}. Order: { doAttribute.Order}.");
                optionsList.Add(new Models.DisplayOption
                {
                    Name = optionField.GetValue(null) as string,
                    RenderingTag = doAttribute.RenderingTag,
                    IconClass = doAttribute.IconClass,
                    Order = doAttribute.Order
                });
            }

            return optionsList;
        }

        private static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
