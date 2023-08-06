using System.Windows.Input;
using VKPRApp.Commands;
using VKPRApp.Shared.Services;
using VKPRApp.Stores.Authenticators;

namespace VKPRApp.ViewModels
{
    [QueryProperty(nameof(PostUrl), "PostUrl")]
    [QueryProperty(nameof(TaskName), "TaskName")]
    public class RepostPaymentViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticator _authenticator;

        public RepostPaymentViewModel(IUserService userService, IAuthenticator authenticator)
        {
            _userService = userService;
            _authenticator = authenticator;

            NavigateBackCommand = new RelayCommand(async obj => await GoBack());
            PayCommand = new RelayCommand(async obj => await Pay());
        }

        public ICommand NavigateBackCommand { get; }
        public ICommand PayCommand { get; }
        public string Sum => $"{RepostsCount*15 + CommentsCount*9} рублей";

        private string _taskName = string.Empty;

        public string TaskName
        {
            get => _taskName;
            set => Set(ref _taskName, value);
        }

        private string _postUrl = string.Empty;

        public string PostUrl
        {
            get => _postUrl;
            set => Set(ref _postUrl, value);
        }

        private int _repostsCount = 0;

        public int RepostsCount
        {
            get => _repostsCount;
            set
            {
                int clampedValue = value > 100 ? 100 : value < 0 ? 0 : value;
                _repostsCount = clampedValue;
                OnPropertyChanged(nameof(RepostsCount));
                OnPropertyChanged(nameof(Sum));
            }
        }

        private int _commentsCount = 0;

        public int CommentsCount
        {
            get => _commentsCount;
            set
            {
                int clampedValue = value > 100 ? 100 : value < 0 ? 0 : value;
                _commentsCount = clampedValue;
                OnPropertyChanged(nameof(CommentsCount));
                OnPropertyChanged(nameof(Sum));
            }
        }

        private async Task Pay()
        {
            int sum = int.Parse(Sum.Split(" ")[0]);

            if (CommentsCount == 0 && RepostsCount == 0 || _authenticator.CurrentUser.Wallet < sum || _authenticator.CurrentUser.Stack+sum > _authenticator.CurrentUser.Wallet)
                return;

            _authenticator.CurrentUser.Stack += sum;
            await _userService.UpdateUser(_authenticator.CurrentUser);

            List<Shared.Models.User> users = await _userService.GetUsers();

            Shared.Models.Task commentTask = new Shared.Models.Task(PostUrl, 3, CommentsCount, _authenticator.CurrentUser.VKUser.UserId, _authenticator.CurrentUser.VKUser.ApiKey, 9) { TaskType = Shared.Models.TaskType.Comment };
            Shared.Models.Task repostTask = new Shared.Models.Task(PostUrl, 5, RepostsCount, _authenticator.CurrentUser.VKUser.UserId, _authenticator.CurrentUser.VKUser.ApiKey, 15) { TaskType = Shared.Models.TaskType.Repost };

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].VKUser.UserId == _authenticator.CurrentUser.VKUser.UserId) continue;

                if(CommentsCount > 0 && users[i].Tasks.FirstOrDefault(t => t.TaskType == Shared.Models.TaskType.Comment) == null)
                {
                    users[i].Tasks.Add(commentTask);
                    await _userService.UpdateUser(users[i]);
                }

                if (RepostsCount > 0 && users[i].Tasks.FirstOrDefault(t => t.TaskType == Shared.Models.TaskType.Repost) == null)
                {
                    users[i].Tasks.Add(repostTask);
                    await _userService.UpdateUser(users[i]);
                }
            }

            await GoBack();
        }
    }
}
