using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamToDo.Helpers;
using TeamToDo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamToDo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTaskPage : ContentPage
    {
        private Models.AppTask _task;

        public CreateTaskPage(object item)
        {

            InitializeComponent();
            if (SessionState.CurrentAccessLevel == 0 || SessionState.CurrentAccessLevel == 3)
            {
                RolePicker.IsVisible = (SessionState.CurrentUser.AccessLevel >= 5) ? true : false;
                CreateButton.IsVisible = false;
                TaskEntry.IsEnabled = false;
                PriorityPicker.IsEnabled = false;
                UserPicker.IsEnabled = false;
                DeadlineDatePicker.IsEnabled = false;
                DeadlineTimePicker.IsEnabled = false;
            }
            else
            {
                Label.Text = "Do you finish?";
                CreateButton.Text = "Update Task";
                CloseButton.IsVisible = true;
            }
            if (SessionState.CurrentAccessLevel == 0 || SessionState.CurrentAccessLevel == 1
                || SessionState.CurrentAccessLevel == 3 || SessionState.CurrentAccessLevel == 4)
            {
                CloseButton.IsVisible = false;
            }

            if (!(item is Models.AppTask task)) return;
            _task = task;

            TaskEntry.Text = task.TaskTitle;
            RolePicker.Title = task.Role;
            string title = task.Priority.Substring(0, task.Priority.Length - 4);
            title = title.Substring(0, 1).ToUpper() + title.Substring(1, title.Length - 1);
            UserPicker.Title = task.User;
            PriorityPicker.Title = title;
            DeadlineDatePicker.Date = task.Deadline.Date;
            DeadlineTimePicker.Time = task.Deadline.TimeOfDay;
            TaskIdEntry.Text = task.Id;
            TaskIdEntry.IsVisible = false;
        }

        public CreateTaskPage()
        {
            InitializeComponent();
            RolePicker.IsVisible = (SessionState.CurrentUser.AccessLevel >= 5) ? true : false;
            CloseButton.IsVisible = false;
        }

        private async void CreateTask_Button_Clicked(object sender, System.EventArgs e)
        {
            string taskTitle = TaskEntry.Text;
            int roleId = (RolePicker.SelectedIndex == -1) ? SessionState.CurrentRoleId : RolePicker.SelectedIndex;
            int priority = PriorityPicker.SelectedIndex;
            string user = (string)UserPicker.SelectedItem;
            var deadineDate = DeadlineDatePicker.Date;
            var deadlineTime = DeadlineTimePicker.Time;

            var deadlineDateTime = new DateTime(deadineDate.Year, deadineDate.Month, deadineDate.Day,
                deadlineTime.Hours, deadlineTime.Minutes, deadlineTime.Seconds);

            if (String.IsNullOrEmpty(taskTitle))
            {
                await DisplayAlert("Action Error", "Empty task title", "OK");
                return;
            }

            var task = new Models.Task
            {
                Id = (CreateButton.Text == "Update Task") ? TaskIdEntry.Text : Guid.NewGuid().ToString(),
                TaskTitle = taskTitle,
                RoleId = roleId,
                Priority = priority,
                User = user,
                Username = SessionState.CurrentUser.Username,
                Deadline = deadlineDateTime,
            };

            if (CreateButton.Text == "Update Task")
            {
                TaskHelper.UpdateTask(task);
            }
            else
            {
                TaskHelper.AddTask(task);
            }
            await DisplayAlert("Excellent!!", "Task was created successfully", "OK");
            await Navigation.PushAsync(new TasksListPage());
        }

        private async void CloseTask_Button_Clicked(object sender, System.EventArgs e)
        {
            TaskHelper.CloseTask(_task.Id);
            await DisplayAlert("Wow!", "Task was closed successfully", "OK");
            await Navigation.PushAsync(new TasksListPage());
        }
    }
}