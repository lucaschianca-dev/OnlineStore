using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models;

[FirestoreData]
public class PendingUser
{
    [FirestoreProperty]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [FirestoreProperty]
    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    public string Email { get; set; } = string.Empty;

    [FirestoreProperty]
    [Required(ErrorMessage = "Nome completo é obrigatório.")]
    [MinLength(3, ErrorMessage = "O Nome Completo deve ter pelo menos 3 caracteres.")]
    [MaxLength(40, ErrorMessage = "O Nome Completo deve ter no máximo 40 caracteres.")]
    public string FullName { get; set; } = string.Empty;

    [FirestoreProperty]
    [Required(ErrorMessage = "Role é obrigatória.")]
    [RegularExpression("^(ADMIN|CLIENT)$", ErrorMessage = "Role deve ser ADMIN ou CLIENT.")]
    public string Role { get; set; } = "CLIENT";

    [FirestoreProperty]
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
    public string Password { get; set; } = string.Empty;

    [FirestoreProperty]
    public DateTime CreationAt { get; set; } = DateTime.UtcNow;

    [FirestoreProperty]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}