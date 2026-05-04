using Application.Common.Abstraction;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Implementation
{
    public class HangfireJob
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<HangfireJob> _logger;

        public HangfireJob(IServiceScopeFactory scopeFactory, ILogger<HangfireJob> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        [DisableConcurrentExecution(timeoutInSeconds: 300)]
        [AutomaticRetry(Attempts = 3, DelaysInSeconds = new[] { 60, 300, 900 })]
        public async Task ProcessSessions()
        {
            _logger.LogInformation("HangfireJob ProcessSessions started at {Time}", DateTime.UtcNow);
            
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var faceService = scope.ServiceProvider.GetRequiredService<IFacePrintService>();

                await faceService.AssignUsersToSession();
                
                _logger.LogInformation("HangfireJob ProcessSessions completed successfully at {Time}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HangfireJob ProcessSessions failed at {Time}", DateTime.UtcNow);
                throw;
            }
        }
    }
}
