using AutoMapper;
using FirebaseAdmin.Auth;
using OnlineStore.DTOs.User;
using OnlineStore.DTOs.User.RegisterUserInput;
using OnlineStore.Models;
using OnlineStore.Repositories;
using System;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public class AuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AuthService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<string> RegisterAsync(RegisterUserInput input)
        {
            try
            {
                var userRecordArgs = new UserRecordArgs()
                {
                    Email = input.Email,
                    Password = input.Password
                };
                var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

                var newUser = _mapper.Map<User>(input);
                newUser.Id = userRecord.Uid;

                await _userRepository.AddUserAsync(newUser);

                return userRecord.Uid;
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Erro ao registrar: {ex.Message}");
            }
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
            var user = await _userRepository.GetUserByIdAsync(userRecord.Uid);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado no banco de dados.");
            }

            return user;
        }

        public async Task<string> GenerateTokenAsync(string uid)
        {
            return await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
        }
    }
}