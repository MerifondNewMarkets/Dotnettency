using System;

namespace Dotnettency
{
    interface IStructureMapHostContainerConfigurator
    {
        Action<StructureMap.ConfigurationExpression> ConfigureHostContainer { get; }
    }
}
