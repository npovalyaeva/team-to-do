using System;
using System.ComponentModel;

using TeamToDo.Converters;
using TeamToDo.Helpers;
using TeamToDo.Models;
using TeamToDo.Pages;

using Xamarin.Forms;

namespace TeamToDo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SignIn_Button_Clicked(object sender, System.EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                await DisplayAlert("", "Empty username or password", "OK");
                return;
            }

            var user = await UserHelper.GetUserByUsernameAndPassword(username, Hash.FindHash(password));
            if (user == null)
            {
                await DisplayAlert("", "Incorrect username or password", "OK");
                return;
            }
            
            SessionState.CurrentUser = user;
            SessionState.CurrentRoleId = user.RoleId;
            SessionState.CurrentAccessLevel = user.AccessLevel;

            await Navigation.PushAsync(new ListOfTabsPage());
        }

        private async void SignUp_Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }
}
