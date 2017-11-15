using System;
using System.Threading.Tasks;

namespace Dotnettency.Container
{
    public abstract class TenantContainerFactory<TTenant> : ITenantContainerFactory<TTenant>
        where TTenant : class
    {
        private readonly IServiceProvider _serviceProvider;

        public TenantContainerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ITenantContainerAdaptor> Get(TTenant currentTenant)
        {
            var builtContainer = await BuildContainer(currentTenant);
            return builtContainer;
        }

        protected abstract Task<ITenantContainerAdaptor> BuildContainer(TTenant currentTenant);
    }
}
