using System.Windows.Input;
using VKPRApp.Commands;
using VKPRApp.Services.UserActionServices;
using VKPRApp.Shared.Services;
using VKPRApp.Stores.Authenticators;
using VKPRApp.Views;

namespace VKPRApp.ViewModels
{
    public class InfoViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IUserActionService _userActionService;
        private readonly IUserService _userService;

        public InfoViewModel(IAuthenticator authenticator, IUserService userService, IUserActionService userActionService)
        {
            _authenticator = authenticator;
            _authenticator.UserChanged += OnUserChanged;

            _userActionService = userActionService;
            _userActionService.ActionMade += OnUserChanged;

            _userService = userService;

            GoToLinkCardPageCommand = new RelayCommand(async obj => await GoToLinkCardPage());
            UnlinkCardCommand = new RelayCommand(async obj => await UnlinkCard());
            SubscribeCommand = new RelayCommand(async obj => await Subscribe());
            SwitchRenewalCommand = new RelayCommand(async obj => await SwitchRenewal());
        }

        public ICommand GoToLinkCardPageCommand { get; }
        public ICommand UnlinkCardCommand { get; }
        public ICommand SubscribeCommand { get; }
        public ICommand SwitchRenewalCommand { get; }
        public Shared.Models.User User => _authenticator.CurrentUser;
        public bool IsCardLinked => User?.BankCard != null;
        public bool IsSubscribed => User?.Subscription != null;
        public bool IsAutoRenewal => !IsSubscribed ? false : User.Subscription.AutoRenewal;
        public string FullName => $"{User?.SecondName} {User?.Name}";
        public string Wallet => $"{User?.Wallet}₽";
        public string ExpireDate => IsSubscribed ? $"{User?.Subscription.ExpireDate.Day}/{User.Subscription.ExpireDate.Month}" : string.Empty;

        private async Task GoToLinkCardPage()
        {
            await Shell.Current.GoToAsync(nameof(LinkCardPage));
        }

        private async Task SwitchRenewal()
        {
            bool value = !_authenticator.CurrentUser.Subscription.AutoRenewal;
            _authenticator.CurrentUser = await _userService.UpdateAutoRenewal(_authenticator.CurrentUser.VKUser.UserId, value);
        }

        private async Task UnlinkCard()
        {
            _authenticator.CurrentUser = await _userService.SetBankCardToNull(_authenticator.CurrentUser.VKUser.UserId);
        }

        private async Task Subscribe()
        {
            if (_authenticator.CurrentUser.BankCard is null)
                return;

            _userActionService.Subscribe();
            await _userService.UpdateUser(_authenticator.CurrentUser);
        }

        private void OnUserChanged()
        {
            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(FullName));
            OnPropertyChanged(nameof(IsCardLinked));
            OnPropertyChanged(nameof(IsSubscribed));
            OnPropertyChanged(nameof(ExpireDate));
            OnPropertyChanged(nameof(IsAutoRenewal));
            OnPropertyChanged(nameof(Wallet));
        }
    }
}
