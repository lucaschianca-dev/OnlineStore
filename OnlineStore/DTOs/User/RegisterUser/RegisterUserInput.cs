using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTOs.User.RegisterUserInput;

public class RegisterUserInput
{
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
    [MinLength(3, ErrorMessage = "O Nome Completo deve ter pelo menos 3 caracteres.")]
    [MaxLength(40, ErrorMessage = "O Nome Completo deve ter no máximo 40 caracteres.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "O campo Role é obrigatório.")]
    [RegularExpression("^(ADMIN|CLIENT)$", ErrorMessage = "Role deve ser ADMIN ou CLIENT.")]
    public string Role { get; set; }

    public RegisterUserInput(string email, string password, string fullName, string role)
    {
        Email = email;
        Password = password;
        FullName = fullName;
        Role = role;
    }
}
