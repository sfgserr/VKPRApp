using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class NotificationsPage : ContentPage
{
	public NotificationsPage(NotificationsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}