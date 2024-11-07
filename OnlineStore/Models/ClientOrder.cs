using Google.Cloud.Firestore;
using OnlineStore.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models;

[FirestoreData]
public class ClientOrder
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty]
    public string UserId { get; set; } // Firebase Authentication UserId

    [FirestoreProperty]
    public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();

    [FirestoreProperty]
    public double TotalAmount { get; set; }

    [FirestoreProperty]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [FirestoreProperty]
    [Required(ErrorMessage = "Status é obrigatória.")]
    public string Status { get; set; }

    public OrderStatus OrderStatusEnum
    {
        get => Enum.Parse<OrderStatus>(Status); // Converte string para enum ao acessar
        set => Status = value.ToString(); // Converte enum para string ao definir
    }
}
