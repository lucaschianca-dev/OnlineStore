using Google.Cloud.Firestore;

namespace OnlineStore.Models;

[FirestoreData]
public class ItemOrder
{
    [FirestoreProperty]
    public string ItemId { get; set; }

    [FirestoreProperty]
    public int Quantity { get; set; }

    [FirestoreProperty]
    public decimal UnitPrice { get; set; }

    [FirestoreProperty]
    public decimal TotalPrice => Quantity * UnitPrice;
}
