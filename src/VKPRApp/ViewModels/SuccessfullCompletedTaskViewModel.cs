using System.Windows.Input;
using VKPRApp.Commands;
using VKPRApp.Views;

namespace VKPRApp.ViewModels
{
    [QueryProperty(nameof(Reward), "Reward")]
    public class SuccessfullCompletedTaskViewModel : ViewModelBase
    {
        public SuccessfullCompletedTaskViewModel()
        {
            GoToTaskPageCommand = new RelayCommand(async obj => await GoToTaskPage());
        }

        public ICommand GoToTaskPageCommand { get; }
        public string RewardMessage => $"{Reward}₽";

        private int _reward = 0;

        public int Reward
        {
            get => _reward;
            set 
            {
                Set(ref _reward, value);
                OnPropertyChanged(nameof(RewardMessage));
            }
        }

        private async Task GoToTaskPage()
        {
            await Shell.Current.GoToAsync("//TasksPage");
        }
    }
}
