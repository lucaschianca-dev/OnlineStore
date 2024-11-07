using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProtectedController : ControllerBase
{
    [HttpGet("protected-endpoint")]
    public async Task<IActionResult> ProtectedEndpoint()
    {
        string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        try
        {
            // Verificar e decodificar o token
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            string uid = decodedToken.Uid;

            // O usuário é autenticado, prossiga com a lógica da API
            return Ok(new { message = "Você está autenticado!", uid });
        }
        catch (FirebaseAuthException ex)
        {
            return Unauthorized(new { message = "Token inválido", error = ex.Message });
        }
    }
}