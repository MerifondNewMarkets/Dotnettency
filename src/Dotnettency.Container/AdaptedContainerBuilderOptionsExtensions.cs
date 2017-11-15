using Dotnettency.Container;
using System;

namespace Dotnettency
{
    public static class AdaptedContainerBuilderOptionsExtensions
    {
        public static AdaptedContainerBuilderOptions<TTenant> OnHostContainerStartup<TTenant>(
            this AdaptedContainerBuilderOptions<TTenant> @this,
            Action<IServiceProvider> callback)
            where TTenant : class
        {
            @this.OnHostContainerBuilt = callback;
            return @this;
        }

        public static AdaptedContainerBuilderOptions<TTenant> OnTenantContainerStartup<TTenant>(
            this AdaptedContainerBuilderOptions<TTenant> @this,
            Action<TTenant, IServiceProvider> callback)
            where TTenant : class
        {
            @this.OnTenantContainerBuilt = callback;
            return @this;
        }
    }
}
