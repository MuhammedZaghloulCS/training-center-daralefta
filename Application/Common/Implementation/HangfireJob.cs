using Application.Common.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Implementation
{
    public class HangfireJob
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public HangfireJob(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task ProcessSessions()
        {
            using var scope = _scopeFactory.CreateScope();

            var faceService = scope.ServiceProvider.GetRequiredService<IFacePrintService>();

            await faceService.AssignUsersToSession();
        }
    }
}
