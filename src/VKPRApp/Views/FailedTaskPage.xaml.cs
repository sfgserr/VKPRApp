using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class FailedTaskPage : ContentPage
{
	public FailedTaskPage(FailedTaskViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}