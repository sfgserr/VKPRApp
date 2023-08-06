using System.Windows.Input;
using VKPRApp.Commands;

namespace VKPRApp.ViewModels
{
    public class FailedTaskViewModel : ViewModelBase
    {
        public FailedTaskViewModel()
        {
            GoBackCommand = new RelayCommand(async obj => await GoBack());
        }

        public ICommand GoBackCommand { get; }
    }
}
