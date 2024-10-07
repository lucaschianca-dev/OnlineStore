﻿using Google.Cloud.Firestore;
using OnlineStore.Models;

namespace OnlineStore.Repositories.ClientOrderRepository;

public class ClientOrderRepository : IClientOrderRepository
{
    private readonly FirestoreDb _firestoreDb;

    public ClientOrderRepository(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }

    public async Task<string> AddClientOrderAsync(ClientOrder clientOrder)
    {
        CollectionReference ordersRef = _firestoreDb.Collection("ClientOrders");
        DocumentReference addedDoc = await ordersRef.AddAsync(clientOrder);
        return addedDoc.Id; // Retorna o ID da nova ordem
    }

    public async Task<ClientOrder> GetClientOrderByUserIdAsync(string userId)
    {
        var query = _firestoreDb.Collection("ClientOrders").WhereEqualTo("UserId", userId);
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        return snapshot.Documents.Count > 0 ? snapshot.Documents[0].ConvertTo<ClientOrder>() : null;
    }

    public async Task UpdateClientOrderAsync(ClientOrder order)
    {
        DocumentReference orderRef = _firestoreDb.Collection("ClientOrders").Document(order.Id);
        await orderRef.SetAsync(order, SetOptions.MergeAll);
    }

    public async Task<List<ClientOrder>> GetAllClientOrdersAsync()
    {
        QuerySnapshot snapshot = await _firestoreDb.Collection("ClientOrders").GetSnapshotAsync();
        var orders = new List<ClientOrder>();
        foreach (var doc in snapshot.Documents)
        {
            if (doc.Exists)
            {
                orders.Add(doc.ConvertTo<ClientOrder>());
            }
        }
        return orders;
    }

    public async Task DeleteClientOrderAsync(string id)
    {
        DocumentReference orderRef = _firestoreDb.Collection("ClientOrders").Document(id);
        await orderRef.DeleteAsync();
    }
}