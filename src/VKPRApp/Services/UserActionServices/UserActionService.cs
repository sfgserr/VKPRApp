using VKPRApp.Shared.Models;
using VKPRApp.Stores.Authenticators;

namespace VKPRApp.Services.UserActionServices
{
    public class UserActionService : IUserActionService
    {
        public event Action ActionMade;

        private readonly IAuthenticator _authenticator;

        public UserActionService(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public void LinkBankCard(BankCard bankCard)
        {
            _authenticator.CurrentUser.BankCard = bankCard;
            ActionMade?.Invoke();
        }

        public void Subscribe()
        {
            _authenticator.CurrentUser.Subscription = new Subscription(DateTime.Now.AddMonths(1), false);
            ActionMade?.Invoke();
        }
    }
}
