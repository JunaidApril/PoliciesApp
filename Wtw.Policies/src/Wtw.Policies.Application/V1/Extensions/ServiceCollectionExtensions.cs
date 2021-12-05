using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wtw.Policies.Application.BFF.Interfaces;
using Wtw.Policies.Application.BFF.Services;

namespace Wtw.Policies.Application.BFF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPolicyService, PolicyService>();

        }
    }
}
