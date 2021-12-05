using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Wtw.Policies.Infrastructure.Configurations;
using Wtw.Policies.Infrastructure.Data;
using Wtw.Policies.Infrastructure.Repositories;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Infrastructure
{
    public static class Startup
    {
        public static void ConfigureServices_Infrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // infrastructure
            services.AddDbContext<PoliciesContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<PoliciesRepository>>();
            services.AddSingleton(typeof(ILogger), logger);

            var logger2 = serviceProvider.GetService<ILogger<PolicyHolderRepository>>();
            services.AddSingleton(typeof(ILogger), logger2);

            services.AddScoped<IPoliciesRepository, PoliciesRepository>();
            services.AddScoped<IPolicyHolderRepository, PolicyHolderRepository>();
        }

        public static void Configure_Infrastructure(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<PoliciesContext>>();
                logger.LogDebug("Applying database migrations...");

                var context = scope.ServiceProvider.GetService<PoliciesContext>();

                context.Database.Migrate();
            }
        }
    }
}
