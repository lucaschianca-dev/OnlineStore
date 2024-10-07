using Microsoft.AspNetCore.Mvc;
using OnlineStore.DTOs.User.UpdateUser;
using OnlineStore.Models;
using OnlineStore.Services.UserService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userService.AddUserAsync(user);
            return Ok("Usuário adicionado com sucesso.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUserPartially(string userId, [FromBody] UpdateUserInput input)
        {
            // Verifica se o modelo de entrada é válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Chama o serviço para atualizar o usuário de forma parcial
            var result = await _userService.UpdateUserAsync(userId, input);

            if (!result)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            // Retorna uma resposta de sucesso
            return Ok(new { message = "Usuário atualizado com sucesso" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("Usuário excluído com sucesso.");
        }
    }
}
