using System.Collections.Generic;

namespace Perficient.Infrastructure.DisplayOptions.Interface
{
    /// <summary>
    /// Inherit from IDisplayOptionsProvider to define DisplayOptions in GetList method.
    /// </summary>
    public interface IDisplayOptionsProvider
    {
        IEnumerable<Models.DisplayOption> GetList();
    }
}
