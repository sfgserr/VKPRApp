using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage(WelcomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}