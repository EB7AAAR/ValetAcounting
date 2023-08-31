using ValetAcounting.View;

namespace ValetAcounting;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(RecordsPage), typeof(RecordsPage));

    }
}
