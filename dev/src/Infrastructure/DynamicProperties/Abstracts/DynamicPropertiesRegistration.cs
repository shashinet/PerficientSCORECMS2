using Perficient.Infrastructure.DynamicProperties.Models;
using System;
using System.Collections.Generic;

namespace Perficient.Infrastructure.DynamicProperties.Abstracts
{
    public abstract class DynamicPropertiesRegistration
    {
        public List<DynamicPropertyRegistratorModel> DynamicProperties { get; set; } = new List<DynamicPropertyRegistratorModel>();

        public abstract Type ForType { get; set; }

        /// <summary>
        ///  Allows you to specify if other content types need to use this dynamic property registraion.
        ///  For example, if you use a block as a property on a page type.
        ///  The key should be the Content Type to use this dynamic property registration on, and the value is the property name to target on the Content Type.
        /// </summary>
        public virtual Dictionary<Type, string[]> OtherRegisteredTypes { get; set; } = new Dictionary<Type, string[]>();

        public abstract void RegisterDynamicProperties();

        public DynamicPropertiesRegistration()
        {
            RegisterDynamicProperties();
        }
    }
}
