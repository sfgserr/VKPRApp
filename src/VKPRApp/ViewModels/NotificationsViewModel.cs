using VKPRApp.Stores.Authenticators;

namespace VKPRApp.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;

        public NotificationsViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _authenticator.UserChanged += OnUserChanged;
        }

        public Shared.Models.User User => _authenticator.CurrentUser;
        public string SecondName => $"{User?.SecondName}!";

        private void OnUserChanged()
        {
            OnPropertyChanged(nameof(User));
        }
    }
}
