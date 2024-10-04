using Google.Cloud.Firestore;

namespace OnlineStore.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string FullName { get; set; }

        [FirestoreProperty]
        public string Role { get; set; }

        public User(){}
    }
}
