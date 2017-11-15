using StructureMap;
using System;

namespace Dotnettency.Container
{
    class StructureMapRequestContainerConfigurator : IStructureMapRequestContainerConfigurator
    {
        public Action<ConfigurationExpression> ConfigureRequestContainer { get; set; }
    }
}
