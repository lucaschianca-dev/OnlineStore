using FirebaseAdmin.Auth;
using OnlineStore.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

public class SendEmailService
{
    private const string SendGridApiKey = "SEND_GRID_API"; // Coloque sua chave de API do SendGrid aqui

    // Método para enviar o email de verificação
    public async Task SendEmailVerificationAsync(PendingUser pendingUser)
    {
        try
        {
            // Usando SendGrid para enviar o email de verificação
            var client = new SendGridClient(SendGridApiKey);
            var from = new EmailAddress("lucaseduardochianca.dev@gmail.com", "webstore"); // Substitua pelo seu email verificado
            var to = new EmailAddress(pendingUser.Email);
            var verifyEmailLink = $"https://localhost:7033/api/Auth/verify-email?uid={pendingUser.Id}"; // Link do seu endpoint

            var subject = "Por favor, verifique seu email";
            var plainTextContent = $"Por favor, verifique seu email clicando no seguinte link: {verifyEmailLink}";
            var htmlContent = $"<strong>Por favor, verifique seu email clicando no link:</strong> <a href='{verifyEmailLink}'>Verificar Email</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Email de verificação enviado para {pendingUser.Email}");
            }
            else
            {
                Console.WriteLine($"Falha ao enviar email de verificação: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao enviar email de verificação: {ex.Message}");
        }
    }
}
