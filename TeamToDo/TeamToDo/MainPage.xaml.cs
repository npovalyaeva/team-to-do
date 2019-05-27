using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamToDo.Converters;
using TeamToDo.Helpers;
using TeamToDo.Models;
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

        int count = 0;
        private async void SignIn_Button_Clicked(object sender, System.EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                await DisplayAlert("Action Error", "Empty username or password", "OK");
                return;
            }

            //var user = await UserHelper.GetUserByUsernameAndPassword(username, password);
            //if (user == null)
            //{
            //    await DisplayAlert("Error", "Invalid login or password", "OK");
            //    Loading.IsRunning = false;
            //    return;
            //}

            var user = new User
            {
                Username = UsernameEntry.Text,
                Password = Hash.FindHash(PasswordEntry.Text),
                RoleId = 1,
                Priority = 8
            };

            UserHelper.AddUser(user);
        }
    }
}
