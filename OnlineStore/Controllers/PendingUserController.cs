using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.PendingUserService;

namespace OnlineStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PendingUserController : ControllerBase
{
    private readonly PendingUserService _pendingUserService;

    public PendingUserController(PendingUserService pendingUserService)
    {
        _pendingUserService = pendingUserService;
    }

    [HttpDelete("cleanup-pending-users")]
    public async Task<IActionResult> CleanupPendingUsers()
    {
        await _pendingUserService.DeleteOldPendingUsersAsync(7);
        return Ok("Pending users older than 7 days have been cleaned up.");
    }

    [HttpGet("older-than")]
    public async Task<IActionResult> GetPendingUsersOlderThan()
    {
        var pendingUsers = await _pendingUserService.GetPendingUsersOlderThanAsync(7);
        return Ok(pendingUsers);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPendingUsers()
    {
        var pendingUsers = await _pendingUserService.GetAllPendingUsersAsync();
        return Ok(pendingUsers);
    }
}

