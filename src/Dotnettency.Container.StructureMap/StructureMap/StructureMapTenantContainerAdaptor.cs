using Dotnettency.Container.StructureMap;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using System;

namespace Dotnettency.Container
{
    public class StructureMapTenantContainerAdaptor : StructureMapServiceProvider, ITenantContainerAdaptor
    {
        private readonly IContainer _container;
        private readonly Guid _id;
        private readonly ILogger<StructureMapTenantContainerAdaptor> _logger;

        public StructureMapTenantContainerAdaptor(ILogger<StructureMapTenantContainerAdaptor> logger, IContainer container, ContainerRole role = ContainerRole.Root) : base(container)
        {
            _logger = logger;
            _container = container;
            _id = Guid.NewGuid();
            Role = role;

            if (role == ContainerRole.Root)
            {
                _logger.LogDebug("Root Container Adaptor Created: {id}, {containerNAme}, {role}", _id, _container.Name, _container.Role);
            }
            else
            {
                _logger.LogDebug("Container Created: {id}, {role}", _id, _container.Name, _container.Role);
            }
        }

        public ContainerRole Role { get; set; }
        public string ContainerName => _container.Name;
        public Guid ContainerId => _id;

        public void Configure(Action<IServiceCollection> configure)
        {
            _container.Configure(_ =>
            {
                _logger.LogDebug("Configuring container: {id}, {containerNAme}, {role}", _id, _container.Name, _container.Role);
                var services = new ServiceCollection();
                configure(services);
                _.Populate(services);
            });
        }

        public void Configure(Action<ConfigurationExpression> configure)
        {
            _logger.LogDebug("Configuring container: {id}, {containerNAme}, {role}", _id, _container.Name, _container.Role);
            _container.Configure(configure);
        }

        /// <summary>
        /// Builds a request container based on the current container
        /// </summary>
        /// <returns></returns>
        public ITenantContainerAdaptor CreateNestedContainer(bool isRequestContainer = false)
        {
            _logger.LogDebug("Creating nested container from container: {id}, {containerNAme}, {role}", _id, _container.Name, _container.Role);

            var nestedContainer = _container.GetNestedContainer();

            if (isRequestContainer)
            {
                var requestContainerConfigurators = _container.GetAllInstances<IStructureMapRequestContainerConfigurator>();
                if (requestContainerConfigurators != null)
                {
                    foreach (var configurator in requestContainerConfigurators)
                    {
                        nestedContainer.Configure(configurator.ConfigureRequestContainer);
                    }
                }
            }

            return new StructureMapTenantContainerAdaptor(_logger, nestedContainer, ContainerRole.Scoped);
        }

        /// <summary>
        /// Builds a tenant container based on the current container
        /// </summary>
        /// <returns></returns>
        public ITenantContainerAdaptor CreateChildContainer()
        {
            _logger.LogDebug("Creating child container from container: {id}, {containerNAme}, {role}", _id, _container.Name, _container.Role);
            
            return new StructureMapTenantContainerAdaptor(_logger, _container.CreateChildContainer(), ContainerRole.Child);
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing of container: {id}, {containerNAme}, {role}", _id, _container.Name, _container.Role);
            _container.Dispose();
        }
    }
}
