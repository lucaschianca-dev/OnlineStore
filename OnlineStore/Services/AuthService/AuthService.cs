using AutoMapper;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using OnlineStore.DTOs.User.RegisterUserInput;
using OnlineStore.Models;
using OnlineStore.Repositories.PendingUserRepository;
using OnlineStore.Repositories.UserRepository;
using System.Text;

namespace OnlineStore.Services.AuthService
{
    public class AuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPendingUserRepository _pendingUserRepository;
        private readonly SendEmailService _sendEmailService;
        private readonly HttpClient _httpClient;

        // Insira sua API key do Firebase aqui (ou carregue-a de uma configuração)
        private const string FirebaseApiKey = "AIzaSyD_ULUGuEN_FO2gkhtEf5KSMrBggNXQZ5E";

        public AuthService(IMapper mapper, IUserRepository userRepository, IPendingUserRepository pendingUserRepository, HttpClient httpClient, SendEmailService sendEmailService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _pendingUserRepository = pendingUserRepository;
            _httpClient = httpClient;
            _sendEmailService = sendEmailService;
        }

        public async Task<string> RegisterAsync(RegisterUserInput input)
        {
            try
            {
                // Criar um PendingUser a partir do RegisterUserInput usando o AutoMapper
                var pendingUser = _mapper.Map<PendingUser>(input);

                // Gerar um ID para o PendingUser (pode ser um GUID ou outro identificador)
                pendingUser.Id = Guid.NewGuid().ToString();

                // Enviar email de verificação (sem criar o usuário no Firebase)
                await _sendEmailService.SendEmailVerificationAsync(pendingUser);

                // Armazenar o usuário na coleção PendingUsers
                await _pendingUserRepository.AddPendingUserAsync(pendingUser);

                return pendingUser.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar: {ex.Message}");
            }
        }

        public async Task VerifyEmailAsync(string uid)
        {
            // Recupera o usuário pendente pela coleção PendingUsers
            var pendingUser = await _pendingUserRepository.GetPendingUserByIdAsync(uid);
            if (pendingUser == null)
            {
                throw new Exception("Usuário pendente não encontrado.");
            }

            // Criar o usuário no Firebase Authentication após a verificação do e-mail
            var userRecordArgs = new UserRecordArgs()
            {
                Email = pendingUser.Email,
                Password = pendingUser.Password, // Usar a senha do PendingUser
                DisplayName = pendingUser.FullName
            };
            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

            // Mapeia o PendingUser para User usando AutoMapper
            var newUser = _mapper.Map<User>(pendingUser);
            newUser.Id = userRecord.Uid;  // Define o ID do usuário como UID do Firebase

            // Persistir o usuário na coleção Users e remover da coleção PendingUsers
            await _userRepository.AddUserAsync(newUser);

            // Aqui, a exclusão está sendo feita pelo Id do PendingUser, que é o documento em Firestore
            await _pendingUserRepository.DeletePendingUserAsync(pendingUser.Id); // Remove da coleção PendingUsers
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var requestBody = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // Faz uma requisição para o endpoint de login do Firebase Authentication
            var response = await _httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={FirebaseApiKey}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseData);

                // Retorna o token JWT para uso
                return jsonResponse.idToken;
            }
            else
            {
                throw new Exception("Email ou senha inválidos");
            }
        }

        public async Task<string> GenerateTokenAsync(string uid)
        {
            return await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
        }
    }
}