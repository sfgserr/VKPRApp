using VKPRApp.ViewModels;

namespace VKPRApp.Views;

public partial class TasksPage : ContentPage
{
	public TasksPage(TasksViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}