using Google.Cloud.Firestore;
using OnlineStore.Models;

namespace OnlineStore.Repositories.PendingUserRepository
{
    public class PendingUserRepository : IPendingUserRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public PendingUserRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task<PendingUser> GetPendingUserByIdAsync(string id)
        {
            DocumentReference pendingUserRef = _firestoreDb.Collection("PendingUsers").Document(id);
            DocumentSnapshot snapshot = await pendingUserRef.GetSnapshotAsync();
            return snapshot.Exists ? snapshot.ConvertTo<PendingUser>() : null;
        }

        public async Task AddPendingUserAsync(PendingUser pendingUser)
        {
            DocumentReference pendingUserRef = _firestoreDb.Collection("PendingUsers").Document(pendingUser.Id);
            await pendingUserRef.SetAsync(pendingUser);
        }

        public async Task DeletePendingUserAsync(string id)
        {
            DocumentReference pendingUserRef = _firestoreDb.Collection("PendingUsers").Document(id);
            await pendingUserRef.DeleteAsync();
        }

        public async Task<List<PendingUser>> GetPendingUsersOlderThanAsync(int days)
        {
            CollectionReference pendingUsersRef = _firestoreDb.Collection("PendingUsers");
            DateTime cutoffDate = DateTime.UtcNow.AddDays(-days);

            Query query = pendingUsersRef.WhereLessThanOrEqualTo("CreationDate", cutoffDate);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<PendingUser> pendingUsers = new List<PendingUser>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                pendingUsers.Add(document.ConvertTo<PendingUser>());
            }

            return pendingUsers;
        }
    }
}
