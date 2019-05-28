using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
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

        public static async Task<User> GetUserByUsernameAndPassword(string username, string passwordHash)
        {
            return (await Firebase
                 .Child("Users")
                 .OnceAsync<User>()).Select(item => new User
                 {
                     Username = item.Object.Username,
                     Password = item.Object.Password,
                     RoleId = item.Object.RoleId,
                     AccessLevel = item.Object.AccessLevel
                 }).FirstOrDefault(x => x.Username == username && x.Password == passwordHash);
        }

        public static async Task<List<AppUser>> GetAllUsers()
        {
            List<string> accessModes = new List<string>()
            {
                "Read role tasks only",
                "Read and update role tasks",
                "Create, read and update role tasks",
                "Read all tasks only",
                "Read and update all tasks",
                "Create, read and update all tasks",
                "Unlimited access rights"
            };

            List<string> roles = new List<string>()
            {
                "Front End developer",
                "Network engineer",
                "Project manager",
                "Quality assurance",
                "Software engineer",
                "Team leader",
                "Web developer"
            };

            return (await Firebase
                 .Child("Users")
                 .OnceAsync<User>()).Select(item => new AppUser
                 {
                     Username = item.Object.Username.ToUpper(),
                     Role = roles[item.Object.RoleId],
                     AccessLevel = accessModes[item.Object.AccessLevel]
                 }).ToList();
        }
    }
}
