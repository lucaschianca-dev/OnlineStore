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
    public double UnitPrice { get; set; }

    [FirestoreProperty]
    public double TotalPrice => Quantity * UnitPrice;
}
