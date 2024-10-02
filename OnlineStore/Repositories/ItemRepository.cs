using Google.Cloud.Firestore;
using Newtonsoft.Json;
using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public ItemRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            Query itemsQuery = _firestoreDb.Collection("Items");
            QuerySnapshot itemsQuerySnapshot = await itemsQuery.GetSnapshotAsync();
            List<Item> ItemsList = new List<Item>();

            foreach (DocumentSnapshot documentSnapshot in itemsQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> item = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(item);
                    Item newItem = JsonConvert.DeserializeObject<Item>(json);
                    newItem.Id = documentSnapshot.Id;
                    ItemsList.Add(newItem);
                }
            }

            return ItemsList;
        }

        public async Task<Item> GetItemByIdAsync(string id)
        {
            DocumentReference documentReference = _firestoreDb.Collection("Items").Document(id);
            DocumentSnapshot documentSnapshot = await documentReference.GetSnapshotAsync();

            if (documentSnapshot.Exists)
            {
                Dictionary<string, object> item = documentSnapshot.ToDictionary();
                string json = JsonConvert.SerializeObject(item);
                Item newItem = JsonConvert.DeserializeObject<Item>(json);
                newItem.Id = documentSnapshot.Id;
                return newItem;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AddItemAsync(Item item)
        {
            CollectionReference collectionReference = _firestoreDb.Collection("Items");
            DocumentReference documentReference = await collectionReference.AddAsync(item);
            return documentReference.Id;
        }

        public async Task<bool> UpdateItemAsync(string id, Item item)
        {
            DocumentReference documentReference = _firestoreDb.Collection("Items").Document(id);
            DocumentSnapshot documentSnapshot = await documentReference.GetSnapshotAsync();
            if (documentSnapshot.Exists)
            {
                await documentReference.SetAsync(item, SetOptions.MergeAll);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            DocumentReference documentReference = _firestoreDb.Collection("Items").Document(id);
            DocumentSnapshot documentSnapshot = await documentReference.GetSnapshotAsync();
            if (documentSnapshot.Exists)
            {
                await documentReference.DeleteAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
