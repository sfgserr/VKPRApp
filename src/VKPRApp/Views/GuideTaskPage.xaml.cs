using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class GuideTaskPage : ContentPage
{
	public GuideTaskPage(GuideTaskViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}