using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models;

[FirestoreData]
public class Item
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    [FirestoreProperty]
    public string Name { get; set; }

    [Required]
    [Range(0.01, 10000.00, ErrorMessage = "O preço deve estar entre 0.01 e 10000.00")]
    [DataType(DataType.Currency)]
    [FirestoreProperty]
    public double Price { get; set; }

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public int StockQuantity { get; set; }

    [FirestoreProperty]
    public bool IsAvailable { get; set; }

    [FirestoreProperty]
    public DateTime CreationAt { get; set; } = DateTime.UtcNow;

    [FirestoreProperty]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}