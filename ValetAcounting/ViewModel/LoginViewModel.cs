using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValetAcounting.View;

namespace ValetAcounting.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        public string webApiKey = "AIzaSyBeUx58Kzv8LsT-OliZTeCBuAIq7TuB6WU";
        private INavigation _navigation;

        public Command RegisterBtn { get; }
        public Command LoginBtn { get; }

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string userPassword;


        public LoginViewModel()
        {
            //this._navigation = navigation;
            RegisterBtn = new Command(RegisterBtnTappedAsync);
            LoginBtn = new Command(LoginBtnTappedAsync);
        }

        private async void LoginBtnTappedAsync(object obj)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                UserName = UserName.Trim();
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserName, UserPassword);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("FreshFirebaseToken", serializedContent);
                //await this._navigation.PushAsync(new Dashboard());
                await Shell.Current.GoToAsync(nameof(RecordsPage), true, new Dictionary<string, object>
            {
                //{"CurrentDateTime",CurrentDateTime },
                //{"Sites",Sites },
                //{"SelectedSiteIndex",SelectedSiteIndex },
                //{"SelectedSite",SelectedSite },
                //{"SqlRecords",SqlRecords },
                //{"SqlMonthRecords",SqlMonthRecords },
                //{"OperatingSqlRecord",OperatingSqlRecord },
                //{"OperatingSqlMonthRecord",OperatingSqlMonthRecord },
                //{"CurrentViewMonthRecord",CurrentViewMonthRecord },
            });
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                return;
            }
        }



        private async void RegisterBtnTappedAsync(object obj)
        {
            //this._navigation.PushAsync(new RegisterPage());
            await Shell.Current.GoToAsync(nameof(RegisterPage), true, new Dictionary<string, object>
            {
                //{"CurrentDateTime",CurrentDateTime },
                //{"Sites",Sites },
                //{"SelectedSiteIndex",SelectedSiteIndex },
                //{"SelectedSite",SelectedSite },
                //{"SqlRecords",SqlRecords },
                //{"SqlMonthRecords",SqlMonthRecords },
                //{"OperatingSqlRecord",OperatingSqlRecord },
                //{"OperatingSqlMonthRecord",OperatingSqlMonthRecord },
                //{"CurrentViewMonthRecord",CurrentViewMonthRecord },
            });
        }
    }
}
