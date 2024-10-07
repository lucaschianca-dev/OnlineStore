using Google.Cloud.Firestore;
using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public UserRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task AddUserAsync(User user)
        {
            DocumentReference userRef = _firestoreDb.Collection("Users").Document(user.Id);
            await userRef.SetAsync(user);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            DocumentReference userRef = _firestoreDb.Collection("Users").Document(id);
            DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();
            return snapshot.Exists ? snapshot.ConvertTo<User>() : null;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var query = _firestoreDb.Collection("Users").WhereEqualTo("Email", email);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            return snapshot.Documents.Count > 0 ? snapshot.Documents[0].ConvertTo<User>() : null;
        }

        public async Task<bool> UpdateUserAsync(string id, User user)
        {
            DocumentReference userRef = _firestoreDb.Collection("Users").Document(id);
            DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                await userRef.SetAsync(user, SetOptions.MergeAll);
                return true;
            }

            return false;
        }

        public async Task DeleteUserAsync(string id)
        {
            DocumentReference userRef = _firestoreDb.Collection("Users").Document(id);
            await userRef.DeleteAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            CollectionReference usersRef = _firestoreDb.Collection("Users");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            List<User> users = new List<User>();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    users.Add(document.ConvertTo<User>());
                }
            }
            return users;
        }
    }
}
