using System.Windows.Input;
using VKPRApp.Commands;
using VKPRApp.Stores.Installers;

namespace VKPRApp.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private readonly IInstaller _installer;

        public WelcomeViewModel(IInstaller installer) 
        {
            _installer = installer;
            _installer.IsLoadingChanged += OnIsLoadingChanged;

            SwitchWebViewVisiblityCommand = new RelayCommand(async obj => await Login());
        }

        public string Source => "https://oauth.vk.com/authorize?client_id=51695549&display=mobile&response_type=token&scope=wall&redirect_uri=https://oauth.vk.com/blank.html";
        public ICommand SwitchWebViewVisiblityCommand { get; }
        public bool IsLoading => _installer.IsLoading;

        private bool _isVisible = false;

        public bool IsVisible
        {
            get => _isVisible;
            set => Set(ref _isVisible, value);
        }

        private async Task Login()
        {
            try
            {
                WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(new WebAuthenticatorOptions()
                {
                    CallbackUrl = new Uri($"{Constants.CallBackScheme}://"),
                    Url = new Uri(new Uri(Constants.BaseUri), "auth/VK")
                });

                await _installer.Install(result.AccessToken, result.Properties["user_id"]);
            }
            catch
            {
                
            }
        }

        private void OnIsLoadingChanged()
        {
            OnPropertyChanged(nameof(IsLoading));
        }
    }
}
