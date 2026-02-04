using FluentValidation;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Implementations.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Dependencies
{
    public static class ModuleApplicationDependencies
    {
        public static IServiceCollection AddedModuleApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            // Register FluentValidation Validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register Validation Behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;
        }
    }
}
