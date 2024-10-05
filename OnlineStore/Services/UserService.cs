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

        public async Task<bool> UpdateUserAsync(string id, UpdateUserInput input)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            // Mapeia as atualizações para o usuário existente usando AutoMapper
            var updatedUser = _mapper.Map(input, existingUser);

            // Atualizar o usuário no Firebase Authentication
            var updateArgs = _mapper.Map<UserRecordArgs>(updatedUser);
            updateArgs.Uid = id; // Define o UID necessário para atualização

            try
            {
                // Chama a API do Firebase para aplicar as atualizações no Firebase Authentication
                await FirebaseAuth.DefaultInstance.UpdateUserAsync(updateArgs);
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Erro ao atualizar o usuário no Firebase Auth: {ex.Message}");
            }

            // Atualiza o usuário no Firestore
            await _userRepository.UpdateUserAsync(id, updatedUser);

            return true;
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
