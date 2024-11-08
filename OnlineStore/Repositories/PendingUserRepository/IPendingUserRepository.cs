﻿using OnlineStore.Models;

namespace OnlineStore.Repositories.PendingUserRepository
{
    public interface IPendingUserRepository
    {
        Task<PendingUser> GetPendingUserByIdAsync(string id);  // Retorna PendingUser, não User
        Task<List<PendingUser>> GetPendingUsersOlderThanAsync(int days);
        Task<List<PendingUser>> GetAllPendingUsersAsync();
        Task AddPendingUserAsync(PendingUser pendingUser);
        Task DeletePendingUserAsync(string email);  // Método para deletar um PendingUser pelo email
    }
}
