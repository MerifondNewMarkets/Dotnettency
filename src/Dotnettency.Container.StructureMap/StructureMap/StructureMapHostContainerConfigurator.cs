using StructureMap;
using System;

namespace Dotnettency.Container
{
    class StructureMapHostContainerConfigurator : IStructureMapHostContainerConfigurator
    {
        public Action<ConfigurationExpression> ConfigureHostContainer { get; set; }
    }
}
