using VKPRApp.Stores.Authenticators;

namespace VKPRApp.Stores.Installers
{
    public class Installer : IInstaller
    {
        public event Action IsLoadingChanged;

        private readonly IAuthenticator _authenticator;

        public Installer(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                IsLoadingChanged?.Invoke();
            }
        }

        public async Task Install(string apiKey, string userId)
        {
            IsLoading = true;

            await _authenticator.LoginAsync(apiKey, userId);

            IsLoading = false;
        }
    }
}
