using System;

namespace Dotnettency.Container
{
    public class AdaptedContainerBuilderOptions<TTenant>
        where TTenant : class
    {
        public AdaptedContainerBuilderOptions(ContainerBuilderOptions<TTenant> parentOptions, Func<ITenantContainerAdaptor> adaptorFactory)
        {
            ContainerBuilderOptions = parentOptions;
            HostContainerAdaptorFactory = adaptorFactory;

            ContainerBuilderOptions.Builder.ServiceProviderFactory = new Func<IServiceProvider>(() =>
            {
                var builtContainer = HostContainerAdaptorFactory();

                OnHostContainerBuilt?.Invoke(builtContainer);

                return builtContainer;
            });
        }

        public ContainerBuilderOptions<TTenant> ContainerBuilderOptions { get; set; }
        public Func<ITenantContainerAdaptor> HostContainerAdaptorFactory { get; set; }
        public Action<IServiceProvider> OnHostContainerBuilt { get; set; }
        public Action<TTenant, IServiceProvider> OnTenantContainerBuilt { get; set; }
    }
}
