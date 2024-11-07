using Google.Cloud.Firestore;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

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
        public List<string> ClientOrderIds { get; set; } = new List<string>(); // IDs dos pedidos relacionados

        [FirestoreProperty]
        public DateTime CreationAt { get; set; } = DateTime.UtcNow;

        [FirestoreProperty]
        public DateTime UpdatedAt { get; set; }

        public User() { }
    }
}
