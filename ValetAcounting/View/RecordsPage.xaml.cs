using Firebase.Auth;
using Newtonsoft.Json;
using ValetAcounting.ViewModel;
using KeyboardExtensions = CommunityToolkit.Maui.Core.Platform.KeyboardExtensions;

namespace ValetAcounting.View;

public partial class RecordsPage : ContentPage
{
    public RecordsPage()
    {
        InitializeComponent();
        BindingContext = new RecordsViewModel(Connectivity.Current);
        GetProfileInfo();


    }

    private void GetProfileInfo()
    {
        var userInfo = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        UserEmail.Text = userInfo.User.Email;
        DisplayName.Text = userInfo.User.DisplayName;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (String.IsNullOrEmpty(CarSumEntry.Text) ||
           String.IsNullOrEmpty(IncomeEntry.Text) ||
           String.IsNullOrEmpty(DailyExpEntry.Text) ||
           String.IsNullOrEmpty(DailyNetEntry.Text) ||
           String.IsNullOrEmpty(TipEntry.Text)) { SendButton.IsEnabled = false; }
        else { SendButton.IsEnabled = true; }
    }


    private async void SendButton_Clicked(object sender, EventArgs e)
    {
        if (KeyboardExtensions.IsSoftKeyboardShowing(DailyNetEntry))
        {
            _ = await KeyboardExtensions.HideKeyboardAsync(DailyNetEntry, default);
        }
        else
        {
            _ = await KeyboardExtensions.ShowKeyboardAsync(DailyNetEntry, default);
        }
        SendButton.IsEnabled = false;
        MainGrid.IsVisible = false;
        AddButton.IsEnabled = true;
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        if (MainGrid.IsVisible == false)
        {
            MainGrid.IsVisible = true;
            AddButton.IsEnabled = true;
            if (String.IsNullOrEmpty(CarSumEntry.Text) ||
           String.IsNullOrEmpty(IncomeEntry.Text) ||
           String.IsNullOrEmpty(DailyExpEntry.Text) ||
           String.IsNullOrEmpty(DailyNetEntry.Text) ||
           String.IsNullOrEmpty(TipEntry.Text)) { SendButton.IsEnabled = false; }
            else { SendButton.IsEnabled = true; }
        }
        else
        {
            MainGrid.IsVisible = false;
            AddButton.IsEnabled = true;
        }


    }
}