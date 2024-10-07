using Google.Cloud.Firestore;

namespace OnlineStore.Models;

[FirestoreData]
public class ItemCart
{
    [FirestoreProperty]
    public string ProductId { get; set; }

    [FirestoreProperty]
    public int Quantity { get; set; }

    [FirestoreProperty]
    public decimal UnitPrice { get; set; }

    [FirestoreProperty]
    public decimal TotalPrice => Quantity * UnitPrice;
}
