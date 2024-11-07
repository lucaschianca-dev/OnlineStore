using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTOs.User.UpdateUser;

public class UpdateUserInput
{
    [MinLength(3, ErrorMessage = "O nome completo deve ter pelo menos 3 caracteres.")]
    [MaxLength(40, ErrorMessage = "O nome completo não pode ter mais de 40 caracteres.")]
    public string? FullName { get; set; }

    [EmailAddress(ErrorMessage = "O email deve ser válido.")]
    public string? Email { get; set; }

    [RegularExpression("^(ADMIN|CLIENT)$", ErrorMessage = "A função deve ser 'ADMIN' ou 'CLIENT'.")]
    public string? Role { get; set; }
}
