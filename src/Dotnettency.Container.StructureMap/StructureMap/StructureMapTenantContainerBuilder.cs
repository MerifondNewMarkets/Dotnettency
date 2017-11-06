using StructureMap;
using System;
using System.Threading.Tasks;

namespace Dotnettency.Container.StructureMap
{
    public class StructureMapTenantContainerBuilder<TTenant> : ITenantContainerBuilder<TTenant>
    {
        private readonly ITenantContainerAdaptor _parentContainer;
        private readonly Action<TTenant, ConfigurationExpression> _configureTenant;

        public StructureMapTenantContainerBuilder(ITenantContainerAdaptor parentContainer, Action<TTenant, ConfigurationExpression> configureTenant)
        {
            _parentContainer = parentContainer;
            _configureTenant = configureTenant;
        }

        public Task<ITenantContainerAdaptor> BuildAsync(TTenant tenant)
        {
            var tenantContainer = _parentContainer.CreateChildContainer();

            (tenantContainer as StructureMapTenantContainerAdaptor).Configure(config =>
            {
                _configureTenant(tenant, config);
            });

            return Task.FromResult(tenantContainer);
        }
    }
}
