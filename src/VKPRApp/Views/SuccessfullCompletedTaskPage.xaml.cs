using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class SuccessfullCompletedTaskPage : ContentPage
{
	public SuccessfullCompletedTaskPage(SuccessfullCompletedTaskViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}