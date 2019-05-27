using System;

using TeamToDo.Converters;
using TeamToDo.Helpers;
using TeamToDo.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamToDo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUserPage : ContentPage
    {
        public CreateUserPage()
        {
            InitializeComponent();
        }

        private async void CreateUser_Button_Clicked(object sender, System.EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            int roleId = RolePicker.SelectedIndex;
            int accessLevel = AccessLevelPicker.SelectedIndex;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                await DisplayAlert("Action Error", "Empty username or password", "OK");
                return;
            }

            var user = new User
            {
                Username = username,
                Password = Hash.FindHash(password),
                RoleId = roleId,
                AccessLevel = accessLevel
            };

            UserHelper.AddUser(user);
            await DisplayAlert("Magically!", "User was created successfully", "OK");
            await Navigation.PushAsync(new UsersListPage());
        }
    }
}