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
    public partial class UsersListPage : ContentPage
    {
        public List<AppUser> Users { get; set; }
        public UsersListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Users = await UserHelper.GetAllUsers();
            UsersList.ItemsSource = Users;
        }

        private void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void CreateNewUser_Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CreateUserPage());
        }
    }
}