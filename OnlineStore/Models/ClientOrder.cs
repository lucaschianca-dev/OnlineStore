using Google.Cloud.Firestore;

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
    public string Status { get; set; } // Ex: Pendente, Enviado, Entregue
}
