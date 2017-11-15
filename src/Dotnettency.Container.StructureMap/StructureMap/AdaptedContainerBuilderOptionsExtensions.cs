using Dotnettency.Container;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;

namespace Dotnettency
{
    public static class AdaptedContainerBuilderOptionsExtensions
    {
        public static AdaptedContainerBuilderOptions<TTenant> ConfigureHostContainer<TTenant>(
            this AdaptedContainerBuilderOptions<TTenant> @this,
            Action<ConfigurationExpression> configurationAction)
            where TTenant : class
        {
            @this.ContainerBuilderOptions.Builder.Services
                .AddTransient<IStructureMapHostContainerConfigurator>((_) => new StructureMapHostContainerConfigurator { ConfigureHostContainer = configurationAction });

            return @this;
        }

        public static AdaptedContainerBuilderOptions<TTenant> ConfigureRequestContainer<TTenant>(
            this AdaptedContainerBuilderOptions<TTenant> @this,
            Action<ConfigurationExpression> configurationAction)
            where TTenant : class
        {
            @this.ContainerBuilderOptions.Builder.Services
                .AddTransient<IStructureMapRequestContainerConfigurator>((_) => new StructureMapRequestContainerConfigurator { ConfigureRequestContainer = configurationAction });

            return @this;
        }
    }
}
