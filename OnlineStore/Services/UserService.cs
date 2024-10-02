using AutoMapper;
using FirebaseAdmin.Auth;
using OnlineStore.DTOs.User;
using OnlineStore.DTOs.User.UpdateUser;
using OnlineStore.Models;
using OnlineStore.Repositories;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task UpdateUserAsync(UpdateUserInput input)
        {
            // Obtém o usuário do Firestore
            var existingUser = await _userRepository.GetUserByIdAsync(input.Id);
            if (existingUser == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            // Mapeia o DTO para a entidade User
            var updatedUser = _mapper.Map(input, existingUser);

            // Atualiza no Firestore
            await _userRepository.UpdateUserAsync(updatedUser);

            // Atualiza no Firebase Authentication
            var userRecordArgs = new UserRecordArgs
            {
                Uid = updatedUser.Id,
                Email = updatedUser.Email,
                DisplayName = updatedUser.FullName
            };

            try
            {
                await FirebaseAuth.DefaultInstance.UpdateUserAsync(userRecordArgs);
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Erro ao atualizar o usuário no Firebase Authentication: {ex.Message}");
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            // Excluir do Firestore
            await _userRepository.DeleteUserAsync(id);

            // Excluir do Firebase Authentication
            try
            {
                await FirebaseAuth.DefaultInstance.DeleteUserAsync(id);
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Erro ao excluir o usuário no Firebase Authentication: {ex.Message}");
            }
        }
    }
}
