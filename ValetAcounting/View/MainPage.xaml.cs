using ValetAcounting.ViewModel;
using KeyboardExtensions = CommunityToolkit.Maui.Core.Platform.KeyboardExtensions;

namespace ValetAcounting.View;

public partial class MainPage : ContentPage
{
    public MainPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

}