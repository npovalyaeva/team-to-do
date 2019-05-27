using System;

using TeamToDo.Converters;
using TeamToDo.Helpers;
using TeamToDo.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TeamToDo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void SignUp_Button_Clicked(object sender, System.EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;
            int roleId = RolePicker.SelectedIndex;
            int accessLevel = 0;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                await DisplayAlert("Action Error", "Empty username or password", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("", "Password and confirm password does not match", "OK");
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
            await Navigation.PushAsync(new MainPage());
        }
    }
}