using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeamToDo.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamToDo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfTabsPage : TabbedPage
    {
        public ListOfTabsPage()
        {
            InitializeComponent();

            ToolbarItems.Add(new ToolbarItem("Logout   ", null, Logout_Button_Clicked));

            if (SessionState.CurrentUser.AccessLevel == 6)
            {
                var usersPage = new NavigationPage(new UsersListPage())
                {
                    Title = "Users"
                };
                Children.Add(usersPage);
            }

            var tasksPage = new NavigationPage(new TasksListPage())
            {
                Title = "Tasks"
            };


            
            Children.Add(tasksPage);
        }

        private async void Logout_Button_Clicked()
        {
            await Navigation.PopToRootAsync(true);
        }
    }
}