using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using OnlineStore.Services.PendingUserService;

public class PendingUserCleanupBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PendingUserCleanupBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var cleanupService = scope.ServiceProvider.GetRequiredService<PendingUserCleanupService>();

                // Limpar usuários pendentes com mais de 7 dias
                await cleanupService.CleanupOldPendingUsersAsync(7);
            }

            // Aguardar um intervalo antes de executar novamente (e.g., 24 horas)
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
