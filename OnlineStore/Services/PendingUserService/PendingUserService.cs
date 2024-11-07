using OnlineStore.Models;
using OnlineStore.Repositories.PendingUserRepository;

namespace OnlineStore.Services.PendingUserService;

public class PendingUserService
{
    private readonly IPendingUserRepository _pendingUserRepository;

    public PendingUserService(IPendingUserRepository pendingUserRepository)
    {
        _pendingUserRepository = pendingUserRepository;
    }

    public async Task DeleteOldPendingUsersAsync(int days = 7)
    {
        var oldPendingUsers = await _pendingUserRepository.GetPendingUsersOlderThanAsync(days);

        foreach (var pendingUser in oldPendingUsers)
        {
            await _pendingUserRepository.DeletePendingUserAsync(pendingUser.Id);
            Console.WriteLine($"PendingUser com email {pendingUser.Email} removido por inatividade.");
        }
    }

    public async Task<List<PendingUser>> GetPendingUsersOlderThanAsync(int days)
    {
        return await _pendingUserRepository.GetPendingUsersOlderThanAsync(days);
    }

    public async Task<List<PendingUser>> GetAllPendingUsersAsync()
    {
        return await _pendingUserRepository.GetAllPendingUsersAsync();
    }
}

