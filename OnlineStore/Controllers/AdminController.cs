using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.PendingUserService;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly PendingUserCleanupService _cleanupService;

    public AdminController(PendingUserCleanupService cleanupService)
    {
        _cleanupService = cleanupService;
    }

    [HttpPost("cleanup-pending-users")]
    public async Task<IActionResult> CleanupPendingUsers()
    {
        await _cleanupService.CleanupOldPendingUsersAsync(7);
        return Ok("Pending users older than 7 days have been cleaned up.");
    }
}