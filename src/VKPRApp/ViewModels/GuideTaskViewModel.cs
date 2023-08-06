using System.Windows.Input;
using VKPRApp.Commands;
using VKPRApp.Services.CheckTaskServices;
using VKPRApp.Services.VkUrlServices;
using VKPRApp.Shared.Services;
using VKPRApp.Stores.Authenticators;
using VKPRApp.Views;

namespace VKPRApp.ViewModels
{   
    [QueryProperty(nameof(Task), "Current")]
    public class GuideTaskViewModel : ViewModelBase
    {
        private readonly ICheckTaskService _checkTaskService;
        private readonly IAuthenticator _authenticator;
        private readonly IUserService _userService;
        private readonly IVkUrlService _urlService;
        private readonly HttpClient _client;

        public GuideTaskViewModel(IAuthenticator authenticator, ICheckTaskService checkTaskService, IUserService userService, HttpClient client, IVkUrlService urlService)
        {
            _authenticator = authenticator;
            _userService = userService;
            _client = client;
            _urlService = urlService;
            _checkTaskService = checkTaskService;

            NavigateBackCommand = new RelayCommand(async obj => await GoBack());
            OpenUrlCommand = new RelayCommand(async obj => await Browser.OpenAsync(Task.PostUrl));
            CheckTaskCommand = new RelayCommand(async obj => await CheckTask());
        }

        public ICommand NavigateBackCommand { get; }
        public ICommand OpenUrlCommand { get; }
        public ICommand CheckTaskCommand { get; }
        public string TaskName => Task?.TaskType == Shared.Models.TaskType.Comment ? "«Комментарий»" : "«Репост»";

        private Shared.Models.Task _task;

        public Shared.Models.Task Task
        {
            get => _task;
            set
            {
                Set(ref _task, value);
                OnPropertyChanged(nameof(TaskName));
            }
        }

        private async Task CheckTask()
        {
            CheckTaskResult result = await _checkTaskService.CheckTask(Task, _authenticator.CurrentUser.VKUser.UserId);

            if (result == CheckTaskResult.Success)
            {
                Shared.Models.User user = await _userService.GetUserByUserId(Task.UserId);

                user.Stack -= Task.Price;
                user.Wallet -= Task.Price;

                await _userService.UpdateUser(user);

                _authenticator.CurrentUser.Wallet += Task.Reward;

                Task.Count--;

                _authenticator.CurrentUser = await _userService.UpdateUser(_authenticator.CurrentUser);
                _authenticator.CurrentUser = await _userService.CompleteTaskCommand(Task.TaskType, _authenticator.CurrentUser.VKUser.UserId);

                await Shell.Current.GoToAsync(nameof(SuccessfullCompletedTaskPage), new Dictionary<string, object>
                {
                    ["Reward"] = Task.Reward
                });
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(FailedTaskPage));
            } 
        }
    }
}
