using Google.Cloud.Firestore;

namespace OnlineStore.Models;

[FirestoreData]
public class ShoppingCart
{
    [FirestoreProperty]
    public string UserId { get; set; } // Relacionado ao Firebase Authentication

    [FirestoreProperty]
    public List<ItemCart> Items { get; set; } = new List<ItemCart>();

    [FirestoreProperty]
    public decimal TotalAmount { get; set; }
}
