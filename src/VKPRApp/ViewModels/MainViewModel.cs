using System.Text.RegularExpressions;
using System.Windows.Input;
using VKPRApp.Commands;
using VKPRApp.Shared.Services;
using VKPRApp.Stores.Authenticators;
using VKPRApp.Views;

namespace VKPRApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticator _authenticator;
        private readonly HttpClient _client;

        public MainViewModel(IAuthenticator authenticator, IUserService userService, HttpClient client)
        {
            _authenticator = authenticator;
            _authenticator.UserChanged += OnUserChanged;

            _userService = userService;
            _client = client;

            GoToRepostPaymentPageCommand = new RelayCommand(async obj => await GoToRepostPaymentPage());
            PromoteCommand = new RelayCommand(async obj => await Promote());

            GetUsersCommand = new RelayCommand(async obj => await GetUsers());
            GetUsersCommand.Execute(null);
        }

        public Shared.Models.User User => _authenticator.CurrentUser;
        public string FullName => $"{User?.SecondName} {User?.Name}";
        public ICommand GetUsersCommand { get; }
        public ICommand GoToRepostPaymentPageCommand { get; }
        public ICommand PromoteCommand { get; }

        private string _postUrlToPromote = string.Empty;

        public string PostUrlToPromote
        {
            get => _postUrlToPromote;
            set => Set(ref _postUrlToPromote, value);
        }

        private string _postUrl = string.Empty;

        public string PostUrl
        {
            get => _postUrl;
            set => Set(ref _postUrl, value);
        }

        private string _countMessage = string.Empty;

        public string CountMessage
        {
            get => _countMessage;
            set => Set(ref _countMessage, value);
        }

        private int _usersCount = 0;

        public int UsersCount
        {
            get => _usersCount;
            set => Set(ref _usersCount, value);
        }

        public async Task PickTaskType(Shared.Models.TaskType taskType)
        {
            CountMessage = taskType switch
            {
                Shared.Models.TaskType.Repost => "Количество репостов",
                Shared.Models.TaskType.Comment => "Количество комментов"
            };
        }

        private async Task GetUsers()
        {
            List<Shared.Models.User> users = await _userService.GetUsers();
            UsersCount = users.Count;
        }

        private async Task Promote()
        {
            if (User.Subscription == null || PostUrlToPromote == string.Empty || User.NextPromote > DateTime.Now)
                return;

            _authenticator.CurrentUser.NextPromote = null;
            await _userService.UpdateUser(_authenticator.CurrentUser);

            if (!PostUrlToPromote.Contains("w=wall"))
                return;

            Regex regex = new Regex(@"\d{9}_\d");

            string[] ids = regex.Match(PostUrlToPromote).Value.Split("_");

            List<Shared.Models.User> users = await _userService.GetUsers();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].VKUser.UserId == User.VKUser.UserId)
                    continue;

                string request = $"https://api.vk.com/method/likes.add?access_token={users[i].VKUser.ApiKey}&owner_id={ids[0]}&item_id={ids[1]}&type=post&v=5.131";

                string message = await _client.GetStringAsync(request);

                if (message.Contains("error")) return;
            }

            PostUrlToPromote = string.Empty;

            _authenticator.CurrentUser.NextPromote = DateTime.Now.AddDays(1);
            await _userService.UpdateUser(_authenticator.CurrentUser);
        }

        private async Task GoToRepostPaymentPage()
        {
            if (PostUrl == string.Empty || CountMessage == string.Empty)
                return;

            if (!(PostUrl.Contains("vk.com") && PostUrl.Contains("w=wall"))) return;

            string s = CountMessage.Split(" ")[1];
            string taskName = s.Remove(s.Length-2);
            taskName = string.Join("", taskName.ToCharArray().Select((c,x) => x == 0 ? c.ToString().ToUpper() : c.ToString()));

            await Shell.Current.GoToAsync(nameof(RepostPaymentPage), new Dictionary<string, object>()
            {
                ["TaskName"] = taskName,
                ["PostUrl"] = PostUrl
            });

            PostUrl = string.Empty;
        }

        private void OnUserChanged()
        {
            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(FullName));
        }
    }
}
