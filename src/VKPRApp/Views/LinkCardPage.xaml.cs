using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class LinkCardPage : ContentPage
{
	public LinkCardPage(LinkCardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}