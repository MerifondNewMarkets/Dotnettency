using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Dotnettency.Container
{
    public class TenantContainerBuilderFactory<TTenant> : TenantContainerFactory<TTenant>
        where TTenant : class
    {
        private readonly IServiceProvider _serviceProvider;

        public TenantContainerBuilderFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<ITenantContainerAdaptor> BuildContainer(TTenant currentTenant)
        {
            var builder = _serviceProvider.GetRequiredService<ITenantContainerBuilder<TTenant>>();
            var container = await builder.BuildAsync(currentTenant);

            container.Configure(x =>
            {
                x.AddSingleton<TTenant>((_) => currentTenant);
                x.AddSingleton<Task<TTenant>>((_) => Task.FromResult(currentTenant));
            });

            var opts = _serviceProvider.GetService<AdaptedContainerBuilderOptions<TTenant>>();
            if (opts != null)
            {
                opts.OnTenantContainerBuilt(currentTenant, container);
            }

            return container;
        }
    }
}
