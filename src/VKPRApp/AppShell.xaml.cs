using VKPRApp.Views;

namespace VKPRApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(RepostPaymentPage), typeof(RepostPaymentPage));
		Routing.RegisterRoute(nameof(GuideTaskPage), typeof(GuideTaskPage));
		Routing.RegisterRoute(nameof(SuccessfullCompletedTaskPage), typeof(SuccessfullCompletedTaskPage));
        Routing.RegisterRoute(nameof(FailedTaskPage), typeof(FailedTaskPage));
        Routing.RegisterRoute(nameof(LinkCardPage), typeof(LinkCardPage));
        Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
    }
}
