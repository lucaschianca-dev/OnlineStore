using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models;

[FirestoreData]
public class Item
{
    [Key]  
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    [FirestoreProperty]
    public string Nome { get; set; }

    [Required]
    [Range(0.01, 10000.00, ErrorMessage = "O preço deve estar entre 0.01 e 10000.00")]
    [DataType(DataType.Currency)]
    [FirestoreProperty]
    public double Preco { get; set; }

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    [FirestoreProperty]
    public string Descricao { get; set; }
}