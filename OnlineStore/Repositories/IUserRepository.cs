using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(string id,User user);
        Task DeleteUserAsync(string id);
        Task<List<User>> GetAllUsersAsync();
    }
}