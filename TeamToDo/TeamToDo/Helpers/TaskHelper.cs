using Firebase.Database;
using Firebase.Database.Query;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamToDo.Models;

namespace TeamToDo.Helpers
{
    public class TaskHelper
    {
        private static readonly FirebaseClient Firebase;

        static TaskHelper()
        {
            Firebase = new FirebaseClient("https://rpodmp-5.firebaseio.com/");
        }

        public static async void AddTask(Models.Task task)
        {
            await Firebase.Child("Tasks").PostAsync(task);
        }

        public static async Task<List<AppTask>> GetTasksByAccessLevelAndRoleId(int accessLevel, int roleId)
        {
            List<string> priority = new List<string>()
            {
                "Low",
                "Medium",
                "High",
                "Critical"
            };

            List<string> priorityColors = new List<string>()
            {
                "#28384c",
                "#366db6",
                "#25ae88",
                "#ed8a19"
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
                 .Child("Tasks")
                 .OnceAsync<Models.Task>()).Select(item => new AppTask
                 {
                     Id = item.Object.Id,
                     TaskTitle = item.Object.TaskTitle,
                     Role = roles[item.Object.RoleId],
                     Priority = priority[item.Object.Priority].ToLower() + ".png",
                     PriorityColor = priorityColors[item.Object.Priority],
                     Deadline = item.Object.Deadline,
                     DeadlineColor = (item.Object.Deadline < DateTime.Now) ? "#dd352e" : "#28384c",
                 })
                 .Where(x => (accessLevel >= 3) ? x.TaskTitle != "" : x.Role == roles[roleId])
                 .OrderBy(x => x.Priority)
                 .ToList();
        }

        public static async void CloseTask(string taskId)
        {
            var toDeleteTask = (await Firebase
                 .Child("Tasks")
                 .OnceAsync<Models.Task>()).FirstOrDefault(a => a.Object.Id == taskId);

            if (toDeleteTask != null)
            {
                await Firebase.Child("Tasks").Child(toDeleteTask.Key).DeleteAsync();
            }
        }
    }
}
