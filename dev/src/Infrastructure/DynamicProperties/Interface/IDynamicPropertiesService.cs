using Perficient.Infrastructure.DynamicProperties.Models;
using System.Collections.Generic;

namespace Perficient.Infrastructure.DynamicProperties.Interface
{
    public interface IDynamicPropertiesService
    {
        List<DynamicPropertyRegistratorModel> RetrieveDynamicPropertiesForType(string typeIdentifier);
    }
}
