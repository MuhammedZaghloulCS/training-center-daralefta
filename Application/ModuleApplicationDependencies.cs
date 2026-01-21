using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Implementations.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
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
            return services;
        }
    }
}
