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

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                // O AuthService lida com o login
                var user = await _authService.LoginAsync(email, password);

                // Gera o token JWT
                var token = await _authService.GenerateTokenAsync(user.Id);
                return Ok(new { token, user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}