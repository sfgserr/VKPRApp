using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class InfoPage : ContentPage
{
	public InfoPage(InfoViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}