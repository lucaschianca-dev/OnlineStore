using Microsoft.AspNetCore.Mvc;
using OnlineStore.DTOs.User.UpdateUser;
using OnlineStore.Models;
using OnlineStore.Services;
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

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserInput input)
        {
            try
            {
                await _userService.UpdateUserAsync(input);
                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("Usuário excluído com sucesso.");
        }
    }
}
