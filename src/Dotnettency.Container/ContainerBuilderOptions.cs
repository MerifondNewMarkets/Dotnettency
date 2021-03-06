﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dotnettency.Container
{
    public class ContainerBuilderOptions<TTenant>
        where TTenant : class
    {
        public ContainerBuilderOptions(MultitenancyOptionsBuilder<TTenant> builder)
        {
            Builder = builder;
            builder.Services.AddSingleton<ITenantContainerFactory<TTenant>, TenantContainerBuilderFactory<TTenant>>();
            builder.Services.AddScoped<ITenantContainerAccessor<TTenant>, TenantContainerAccessor<TTenant>>();
            builder.Services.AddScoped<ITenantRequestContainerAccessor<TTenant>, TenantRequestContainerAccessor<TTenant>>();
        }

        public MultitenancyOptionsBuilder<TTenant> Builder { get; set; }
    }
}
