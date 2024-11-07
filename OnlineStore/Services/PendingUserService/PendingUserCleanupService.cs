using OnlineStore.Repositories.PendingUserRepository;

namespace OnlineStore.Services.PendingUserService;

public class PendingUserCleanupService
{
    private readonly IPendingUserRepository _pendingUserRepository;

    public PendingUserCleanupService(IPendingUserRepository pendingUserRepository)
    {
        _pendingUserRepository = pendingUserRepository;
    }

    public async Task CleanupOldPendingUsersAsync(int days = 7)
    {
        var oldPendingUsers = await _pendingUserRepository.GetPendingUsersOlderThanAsync(days);

        foreach (var pendingUser in oldPendingUsers)
        {
            await _pendingUserRepository.DeletePendingUserAsync(pendingUser.Id);
            Console.WriteLine($"PendingUser com email {pendingUser.Email} removido por inatividade.");
        }
    }
}
