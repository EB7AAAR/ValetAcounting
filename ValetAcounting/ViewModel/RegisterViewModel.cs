using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValetAcounting.View;

namespace ValetAcounting.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel
    {
        public string webApiKey = "AIzaSyBeUx58Kzv8LsT-OliZTeCBuAIq7TuB6WU";

        private INavigation _navigation;

        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private string displayName;
        [ObservableProperty]
        private string adminPassword;

        private string hardPassword = "ValetApp2023";

        public Command RegisterUser { get; }


        public RegisterViewModel()
        {
            //this._navigation = navigation;
            RegisterUser = new Command(RegisterUserTappedAsync);
        }

        private async void RegisterUserTappedAsync(object obj)
        {
            if (AdminPassword != hardPassword)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Wrong Password, Only admin can register users", "Ok");
                return;
            }
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
                Email = Email.Trim();
                DisplayName = DisplayName.Trim();
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password, DisplayName);
                string token = auth.FirebaseToken;
                if (token != null)
                    await App.Current.MainPage.DisplayAlert("Alert", "User Registered successfully", "OK");
                //await this._navigation.PopAsync();
                await Shell.Current.GoToAsync("..");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                return;
            }
        
        }
    }
}
