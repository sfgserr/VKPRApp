using System.Windows.Input;
using VKPRApp.Commands;
using VKPRApp.Helpers;
using VKPRApp.Services.UserActionServices;
using VKPRApp.Shared.Services;
using VKPRApp.Stores.Authenticators;

namespace VKPRApp.ViewModels
{
    public class LinkCardViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IUserActionService _userActionService;
        private readonly IUserService _userService;

        public LinkCardViewModel(IAuthenticator authenticator, IUserService userService, IUserActionService userActionService)
        {
            _authenticator = authenticator;
            _userService = userService;

            _userActionService = userActionService;

            GoBackCommand = new RelayCommand(async obj => await GoBack());
            LinkCardCommand = new RelayCommand(async obj => await LinkCard());
        }

        public ICommand LinkCardCommand { get; }
        public ICommand GoBackCommand { get; }

        private string _cardNumber = string.Empty;

        public string CardNumber
        {
            get => _cardNumber;
            set => Set(ref _cardNumber, value);
        }

        private string _cvv = string.Empty;

        public string CVV
        {
            get => _cvv;
            set => Set(ref _cvv, value);
        }

        private string _expireDate = string.Empty;

        public string ExpireDate
        {
            get => _expireDate;
            set => Set(ref _expireDate, value);
        }

        private async Task LinkCard()
        {
            if (ExpireDate == string.Empty || CardNumber == string.Empty || CVV == string.Empty)
                return;

            Shared.Models.BankCard card = new Shared.Models.BankCard(CardNumber, ExpireDate, CVV);

            if (!card.IsBankCardInfoValid())
                return;

            _userActionService.LinkBankCard(card);
            await _userService.UpdateUser(_authenticator.CurrentUser);

            await GoBack();
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
