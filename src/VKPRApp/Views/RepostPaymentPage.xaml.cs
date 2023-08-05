using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class RepostPaymentPage : ContentPage
{
	public RepostPaymentPage(RepostPaymentViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}