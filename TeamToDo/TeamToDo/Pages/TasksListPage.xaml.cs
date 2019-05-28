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
    public partial class TasksListPage : ContentPage
    {
        public List<AppTask> Tasks { get; set; }
        public TasksListPage()
        {
            InitializeComponent();
            CreateNewTask.IsVisible = (SessionState.CurrentUser.AccessLevel == 2 || SessionState.CurrentUser.AccessLevel >= 5) ? true : false;          
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Tasks = await TaskHelper.GetTasksByAccessLevelAndRoleId(SessionState.CurrentAccessLevel, SessionState.CurrentRoleId);
            TasksList.ItemsSource = Tasks;
        }

        private async void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new CreateTaskPage(e.Item));
            ((ListView)sender).SelectedItem = null;
        }

        private async void CreateNewTask_Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CreateTaskPage());
        }

        private async void Filter_Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new FilterTasksPage());
        }
    }
}