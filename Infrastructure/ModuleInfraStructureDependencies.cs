using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using Infrastructure.Implementations.UnitOfWork;
using Infrastructure.Implementations.UnitOfWork.SysUnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Dependencies
{
    public static class ModuleInfraStructureDependencies
    {
        public static IServiceCollection AddedModuleInfraStructureDependencies(this IServiceCollection services)
        {
            // Here you can add your infrastructure dependencies
            // e.g., services.AddScoped<IYourRepository, YourRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISysUnitOfWork, SysUnitOfWork>();
            return services;
        }
    }
}
