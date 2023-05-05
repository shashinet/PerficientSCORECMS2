using EPiServer.ServiceLocation;
using FastDeepCloner;
using Perficient.Infrastructure.DynamicProperties.Abstracts;
using Perficient.Infrastructure.DynamicProperties.Interface;
using Perficient.Infrastructure.DynamicProperties.Models;
using Perficient.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.DynamicProperties.Services
{
    [ServiceConfiguration(typeof(IDynamicPropertiesService))]
    public class DynamicPropertiesService : IDynamicPropertiesService
    {
        private readonly IEnumerable<DynamicPropertiesRegistration> _dynamicPropertyRegistrators;

        public DynamicPropertiesService(IEnumerable<DynamicPropertiesRegistration> dynamicPropertyRegistrators)
        {
            _dynamicPropertyRegistrators = dynamicPropertyRegistrators ?? Enumerable.Empty<DynamicPropertiesRegistration>();
        }

        public List<DynamicPropertyRegistratorModel> RetrieveDynamicPropertiesForType(string typeIdentifier)
        {
            var dynamicProperties = new List<DynamicPropertyRegistratorModel>();
            var otherDynamicPropertyRegistrations = new List<DynamicPropertiesRegistration>();

            RetrieveDynamicPropertyRegistrations(typeIdentifier, dynamicProperties, otherDynamicPropertyRegistrations);
            ProcessOtherDynamicPropertyRegistrations(typeIdentifier, dynamicProperties, otherDynamicPropertyRegistrations);

            dynamicProperties.ForEach(x => x.ForceEverythingLowercase());
           
            return dynamicProperties;
        }

        private void ProcessOtherDynamicPropertyRegistrations(
            string typeIdentifier,
            List<DynamicPropertyRegistratorModel> dynamicProperties,
            List<DynamicPropertiesRegistration> otherDynamicPropertyRegistrations)
        {
            foreach (var otherPropertyRegistration in otherDynamicPropertyRegistrations)
            {
                var otherRegistratedTypePropertyNames = otherPropertyRegistration
                    .OtherRegisteredTypes
                    .FirstOrDefault(x => x.Key.FullName.Equals(typeIdentifier, StringComparison.InvariantCultureIgnoreCase))
                    .Value;

                foreach (var otherRegistratedTypePropertyName in otherRegistratedTypePropertyNames)
                {
                    var otherDynamicProperties = DeepCloner.Clone(otherPropertyRegistration.DynamicProperties);
                    otherDynamicProperties.ForEach(x => x.PrependValueToPropertyNames(otherRegistratedTypePropertyName));
                    dynamicProperties.AddRangeIfNotNull(otherDynamicProperties);
                }
            }
        }

        private void RetrieveDynamicPropertyRegistrations(
            string typeIdentifier,
            List<DynamicPropertyRegistratorModel> dynamicProperties,
            List<DynamicPropertiesRegistration> otherDynamicPropertyRegistrations)
        {
            foreach (var propertyRegistrator in _dynamicPropertyRegistrators)
            {
                if (propertyRegistrator.ForType.FullName.Equals(typeIdentifier, StringComparison.InvariantCultureIgnoreCase))
                {
                    dynamicProperties.AddRangeIfNotNull(propertyRegistrator.DynamicProperties);
                    continue;
                }

                var allOtherTypeRegistrationNames = propertyRegistrator.OtherRegisteredTypes.Keys.Select(x => x.FullName);
                if (allOtherTypeRegistrationNames.Any(x => x.Equals(typeIdentifier, StringComparison.InvariantCultureIgnoreCase)))
                {
                    otherDynamicPropertyRegistrations.Add(propertyRegistrator);
                    continue;
                }
            }
        }
    }
}
