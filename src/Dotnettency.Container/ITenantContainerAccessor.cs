using System;
using System.Threading.Tasks;

namespace Dotnettency.Container
{
    public interface ITenantContainerAccessor<TTenant>
        where TTenant : class
    {
        Lazy<Task<ITenantContainerAdaptor>> TenantContainer { get; }
        ITenantContainerAccessor<TTenant> WithTenant(TenantDistinguisher tenantDistinguisher);
    }
}
