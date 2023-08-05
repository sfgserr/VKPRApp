using VKPRApp.ViewModels;

namespace VKPRApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

            button.CheckedChanged += async (s, e) => await OnCheckedChanged(vm, s);
            button1.CheckedChanged += async (s, e) => await OnCheckedChanged(vm, s);
        }

        private async Task OnCheckedChanged(MainViewModel vm, object sender)
        {
            Shared.Models.TaskType taskType = ((RadioButton)sender).Content switch
            {
                "Репост" => Shared.Models.TaskType.Repost,
                "Коммент" => Shared.Models.TaskType.Comment,
                _ => throw new ArgumentException()
            }; ;

            await vm.PickTaskType(taskType);
        }
    }
}