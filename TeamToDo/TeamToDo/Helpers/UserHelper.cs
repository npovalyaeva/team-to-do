using Firebase.Database;
using Firebase.Database.Query;

using System.Linq;
using System.Threading.Tasks;

using TeamToDo.Models;


namespace TeamToDo.Helpers
{
    public class UserHelper
    {

        private static readonly FirebaseClient Firebase;

        static UserHelper()
        {
            Firebase = new FirebaseClient("https://rpodmp-5.firebaseio.com/");
        }

        public static async void AddUser(User user)
        {
            await Firebase.Child("Users").PostAsync(user);
        }

        public static async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            return (await Firebase
                 .Child("Users")
                 .OnceAsync<User>()).Select(item => new User
                 {
                     Username = item.Object.Username,
                     Password = item.Object.Password,
                     RoleId = item.Object.RoleId,
                     Priority = item.Object.Priority
                 }).FirstOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
