using Microsoft.Extensions.DependencyInjection;
using Wtw.Policies.Application.V1.Interfaces;
using Wtw.Policies.Application.V1.Services;
using Wtw.Policies.Application.V1.Helpers;
using FluentValidation;
using Wtw.Policies.Application.V1.Commands;
using Wtw.Policies.Application.V1.Queries;

namespace Wtw.Policies.Application.V1.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<IValidationHelper, ValidationHelper>();
            services.AddTransient<IValidator<CreatePolicyHolderCommand>, CreatePolicyHolderCommandValidator>();
            services.AddTransient<IValidator<CreatePolicyCommand>, CreatePolicyCommandValidator>();
            services.AddTransient<IValidator<UpdatePolicyHolderCommand>, UpdatePolicyHolderCommandValidator>();
            services.AddTransient<IValidator<UpdatePolicyCommand>, UpdatePolicyCommandValidator>();
            services.AddTransient<IValidator<DeletePolicyCommand>, DeletePolicyCommandValidator>();
            services.AddTransient<IValidator<DeletePolicyHolderCommand>, DeletePolicyHolderCommandValidator>();
            services.AddTransient<IValidator<GetPolicyHolderQuery>, GetPolicyHolderQueryValidator>();
            services.AddTransient<IValidator<GetPolicyQuery>, GetPolicyQueryValidator>();
        }
    }
}
