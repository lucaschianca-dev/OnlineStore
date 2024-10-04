using Microsoft.AspNetCore.Mvc;
using OnlineStore.DTOs.User;
using OnlineStore.DTOs.User.RegisterUserInput;
using OnlineStore.Services;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserInput input)
        {
            try
            {
                // O AuthService lida com o registro e o mapeamento
                var uid = await _authService.RegisterAsync(input);

                // Gera o token JWT
                var token = await _authService.GenerateTokenAsync(uid);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string uid)
        {
            try
            {
                // Chamar o método do AuthService para verificar o email e criar o usuário no Firebase
                await _authService.VerifyEmailAsync(uid);
                return Ok("Email verificado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na verificação do email: {ex.Message}");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                // Verifica email e senha usando a API REST do Firebase
                var token = await _authService.LoginAsync(email, password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}