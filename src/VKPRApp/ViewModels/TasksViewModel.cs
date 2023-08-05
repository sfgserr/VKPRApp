using System.Windows.Input;
using VKPRApp.Commands;
using VKPRApp.Shared.Services;
using VKPRApp.Stores.Authenticators;
using VKPRApp.Views;

namespace VKPRApp.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IUserService _userService;
        private readonly HttpClient _client;

        public TasksViewModel(IAuthenticator authenticator, IUserService userService, HttpClient client)
        {
            _authenticator = authenticator;
            _authenticator.UserChanged += OnUserChanged;

            _userService = userService;
            _client = client;

            GoToGuideTaskPageCommand = new RelayCommand(async obj => await GoToGuideTaskPage(obj.ToString()));
        }

        public Shared.Models.User User => _authenticator.CurrentUser;
        public bool IsVisibleCommentTask => User is null ? false : User.Tasks.Any(t => t.TaskType == Shared.Models.TaskType.Comment);
        public bool IsVisibleRepostTask => User is null ? false : User.Tasks.Any(t => t.TaskType == Shared.Models.TaskType.Repost);
        public ICommand GoToGuideTaskPageCommand { get; }

        private async Task GoToGuideTaskPage(string taskName)
        {
            Shared.Models.Task task = taskName == "Repost" ? User.Tasks.FirstOrDefault(t => t.TaskType == Shared.Models.TaskType.Repost) : 
                                      taskName == "Comment" ? User.Tasks.FirstOrDefault(t => t.TaskType == Shared.Models.TaskType.Comment) : throw new ArgumentException();

            if (task.Count == 0)
            {
                task = null;
                await _userService.UpdateUser(_authenticator.CurrentUser);
                
                return;
            }

            await Shell.Current.GoToAsync(nameof(GuideTaskPage), new Dictionary<string, object>()
            {
                ["Current"] = task
            });
        }

        private void OnUserChanged()
        {
            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(IsVisibleCommentTask));
            OnPropertyChanged(nameof(IsVisibleRepostTask));
        }
    }
}
