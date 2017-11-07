using System;

namespace Dotnettency
{
    interface IStructureMapRequestContainerConfigurator
    {
        Action<StructureMap.ConfigurationExpression> ConfigureRequestContainer { get; }
    }
}
