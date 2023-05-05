using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Web;
using Microsoft.Extensions.DependencyInjection;
using Perficient.Infrastructure.DynamicProperties.Abstracts;
using Perficient.Infrastructure.DynamicProperties.EditorDescriptors;
using Perficient.Infrastructure.DynamicProperties.UIDescriptors;
using Perficient.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.DynamicProperties.Initialization
{
    [ModuleDependency(typeof(InitializationModule))]
    public class DynamicPropertiesInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic);

            var dynamicPropertyProviders = assemblies
                .SelectMany(a => a.GetLoadableTypes())
                .Where(t => typeof(DynamicPropertiesRegistration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            var addedDescriptorTypes = new HashSet<Type>();

            foreach (var propertyProvider in dynamicPropertyProviders)
            {
                var providerInstance = (DynamicPropertiesRegistration)Activator.CreateInstance(propertyProvider);
                SetupRegistrations(context, addedDescriptorTypes, propertyProvider, providerInstance.ForType);

                if (providerInstance.OtherRegisteredTypes.IsEmpty())
                {
                    continue;
                }

                foreach (var otherRegisteredType in providerInstance.OtherRegisteredTypes)
                {
                    SetupEditorDescriptorRegistrations(context, addedDescriptorTypes, otherRegisteredType.Key);
                }
            }
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        private void SetupRegistrations(ServiceConfigurationContext context, HashSet<Type> addedDescriptorTypes, Type propertyProviderType, Type descriptorType)
        {
            context.Services.Add(typeof(DynamicPropertiesRegistration), propertyProviderType, ServiceLifetime.Transient);
            SetupEditorDescriptorRegistrations(context, addedDescriptorTypes, descriptorType);
        }

        private void SetupEditorDescriptorRegistrations(ServiceConfigurationContext context, HashSet<Type> addedDescriptorTypes, Type descriptorType)
        {
            if (addedDescriptorTypes.Contains(descriptorType))
            {
                return;
            }

            context.Services.AddTransient<ViewConfiguration>(locator => new DynamicPropertyEditorDescriptor(descriptorType));
            context.Services.AddTransient<UIDescriptor>(locator => new DynamicPropertyUIDescriptor(descriptorType));
            addedDescriptorTypes.Add(descriptorType);
        }
    }
}
