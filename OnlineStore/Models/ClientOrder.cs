using Google.Cloud.Firestore;

namespace OnlineStore.Models;

[FirestoreData]
public class ClientOrder
{
    [FirestoreProperty]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [FirestoreProperty]
    public string UserId { get; set; } // Firebase Authentication UserId

    [FirestoreProperty]
    public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();

    [FirestoreProperty]
    public decimal TotalAmount { get; set; }

    [FirestoreProperty]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [FirestoreProperty]
    public string Status { get; set; } // Ex: Pendente, Enviado, Entregue
}
